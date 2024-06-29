using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor {
    public class LineElement : VisualElement{
        private float _length;

        public float Length {
            get => _length;
            set {
                _length = value;
                this.style.width = _length;
            }
        }

        public LineElement(Vector2 from, Vector2 to) {
            //swapping to ensure that we draw the line from left to right
            if (to.x < from.x) {
                (to, from) = (from, to);
            }

            var direction = to - from;
            var angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Length = direction.magnitude;
            var left = from.x;
            var top = from.y;

            this.style.position = Position.Absolute;
            this.style.left = left;
            this.style.top = top;
            this.style.width = Length;
            this.style.transformOrigin = new StyleTransformOrigin(new TransformOrigin(new Length(0, LengthUnit.Percent),
                new Length(50, LengthUnit.Percent)));
            this.transform.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);
        }
    }
}