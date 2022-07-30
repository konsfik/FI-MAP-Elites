using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public partial struct Line_Segment : IEquatable<Line_Segment>
    {
        /// <summary>
        /// Returns the signed angle between this line and the right direction.
        /// The result is in radians, between -PI and PI.
        /// </summary>
        /// <returns></returns>
        public double Q__Orientation()
        {
            Vec2d this_vector = this.Q__To_Vector();
            Vec2d right_vector = Vec2d.right;

            double angle = Vec2d.Signed_Angle_Between(right_vector, this_vector);
            return angle;
        }

        /// <summary>
        /// Returns the deviation of this line's orientation from the X axis.
        /// The inclination is measured between 0 and 1.
        /// </summary>
        /// <returns></returns>
        public double Q__Inclination()
        {
            double orientation = Q__Orientation();
            double fixed_orientation;
            if (orientation > Math.PI / 2.0)
            {
                // orientation between (PI/2) and PI
                fixed_orientation = Math.PI - orientation;
            }
            else if (orientation >= 0)
            {
                // orientation between 0 and PI/2
                fixed_orientation = orientation;
            }
            else if (orientation < -Math.PI / 2.0)
            {
                // orientation between (-PI/2) and -PI
                fixed_orientation = -1.0 * (-Math.PI - orientation);
            }
            else
            {
                fixed_orientation = -1.0 * orientation;
            }
            return fixed_orientation.Q__Mapped(0.0, Math.PI / 2.0, 0.0, 1.0);
        }

        public double Q__Orthogonality()
        {
            double inclination = Q__Inclination();
            if (inclination <= 0.5)
            {
                return inclination.Q__Mapped(0.0, 0.5, 1.0, 0.0);
            }
            else
            {
                return inclination.Q__Mapped(0.5, 1.0, 0.0, 1.0);
            }
        }

        public double Q__Magnitude()
        {
            return (p0 - p1).Q__Magnitude();
        }

        public Vec2d Q__Mid_Point()
        {
            return (p0 + p1) / 2.0;
        }

        public bool Q__Is_Approximately_Connected_To(Line_Segment other_segment, double epsilon)
        {

            return
                this.p0.Q__Approximately_Equal_To(other_segment.p0, epsilon)
                ||
                this.p0.Q__Approximately_Equal_To(other_segment.p1, epsilon)
                ||
                this.p1.Q__Approximately_Equal_To(other_segment.p0, epsilon)
                ||
                this.p1.Q__Approximately_Equal_To(other_segment.p1, epsilon);

        }

        public bool Q__Is_Connected_To(Line_Segment other_segment)
        {
            return
                this.p0 == other_segment.p0
                ||
                this.p0 == other_segment.p1
                ||
                this.p1 == other_segment.p0
                ||
                this.p1 == other_segment.p1;
        }

        public bool Q__Approximately_Equal(Line_Segment other, double epsilon)
        {
            if (this == other)
            {
                return true;
            }
            else
            {
                double m1 = (this.p0 - other.p0).Q__Magnitude();
                double m2 = (this.p1 - other.p1).Q__Magnitude();

                if (m1 < epsilon && m2 < epsilon)
                {
                    return true;
                }

                m1 = (this.p0 - other.p1).Q__Magnitude();
                m2 = (this.p1 - other.p0).Q__Magnitude();
                if (m1 < epsilon && m2 < epsilon)
                {
                    return true;
                }
            }
            return false;
        }

        public Vec2d Q__To_Vector()
        {
            Vec2d from_p0_to_p1 = p1 - p0;
            return from_p0_to_p1;
        }
    }
}
