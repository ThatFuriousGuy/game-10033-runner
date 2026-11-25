using MohawkGame2D;
using System;
using System.Numerics;

namespace sandhu_harsimer_runner.Objects
{
    internal class Spike
    {
        Vector2 position = new Vector2();
        float radius = 15f;

        // Path mode; moves between these points in order, then loops back to the first
        Vector2[] points = new Vector2[0];
        int next_point = 0;

        // Orbit mode; circles around a centre point
        bool orbits = false;
        Vector2 center = new Vector2();
        float orbit_radius = 0f;
        float angle = 0f;

        // Path spike; start_point picks which point it begins on (lets two spikes share a path but stay apart)
        public Spike(float _radius, Vector2[] _points, int start_point = 0)
        {
            radius = _radius;
            points = _points;
            position = points[start_point];
            next_point = (start_point + 1) % points.Length;
        }

        // Orbit spike; start_angle is in radians (0 = right of centre, MathF.PI = left)
        public Spike(float _radius, float center_x, float center_y, float _orbit_radius, float start_angle)
        {
            radius = _radius;
            orbits = true;
            center = new Vector2(center_x, center_y);
            orbit_radius = _orbit_radius;
            angle = start_angle;
            position = center + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * orbit_radius;
        }

        public void Update(float speed)
        {
            if (orbits)
            {
                move_orbit(speed);
            }
            else
            {
                move_path(speed);
            }
            draw_self();
        }

        void move_path(float speed)
        {
            float step = speed * Time.DeltaTime;
            while (step > 0)
            {
                Vector2 target = points[next_point];
                Vector2 to_target = target - position;
                float distance = to_target.Length();
                // This helps keeps the rotating spikes stay a certain distance away from each other
                if (step >= distance)
                {
                    position = target;
                    step -= distance;
                    next_point = (next_point + 1) % points.Length;
                }
                else
                {
                    position += to_target / distance * step;
                    step = 0;
                }
            }
        }

        void move_orbit(float speed)
        {
            // Dividing by the orbit radius keeps the spike moving at the same speed as spikes moving in a straight line
            angle += speed / orbit_radius * Time.DeltaTime;
            position = center + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * orbit_radius;
        }

        void draw_self()
        {
            Draw.LineSize = 0;
            Draw.FillColor = Color.Magenta;
            Draw.Circle(position.X, position.Y, radius);
        }

        // Circle touching rectangle
        // I'm not sure if there was a better way to check for this
        public bool is_touching(Vector2 player_pos, Vector2 player_size)
        {
            float closest_x = Math.Clamp(position.X, player_pos.X, player_pos.X + player_size.X-4f);
            float closest_y = Math.Clamp(position.Y, player_pos.Y, player_pos.Y + player_size.Y-4f);
            float dx = position.X - closest_x;
            float dy = position.Y - closest_y;
            return dx * dx + dy * dy < radius * radius;
        }
    }
}