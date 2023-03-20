namespace MarkerPositions
{
    public class Marker : Label
    {
        private MarkerMediator mediator;
        private Point mouseDownLocation;

        internal void SetMediator(MarkerMediator mediator)
        {
            this.mediator = mediator;
        }

        public Marker()
        {
            Text = "{Drag Me}";
            TextAlign = ContentAlignment.MiddleCenter;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
        }

        private void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Text = Location.ToString();
                Left = e.X + Left - mouseDownLocation.X;
                Top = e.Y + Top - mouseDownLocation.Y;
                mediator.Send(Location, this);
            }
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
            }
        }

        public void ReceiveLocation(Point location)
        {
            var distance = CalcDistance(location);
            if (distance < 100 && BackColor != Color.Red)
            {
                BackColor = Color.Red;
            }
            else if (distance >= 100 && BackColor != Color.Green)
            {
                BackColor = Color.Green;
            }
        }

        private double CalcDistance(Point point)
        {
            return Math.Sqrt(Math.Pow(point.X - Location.X, 2) + Math.Pow(point.Y - Location.Y,2));
        }
    }
}
