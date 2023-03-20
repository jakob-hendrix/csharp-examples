namespace MarkerPositions
{
    public class MarkerMediator
    {
        private List<Marker> markers = new List<Marker>();

        public Marker CreateMarker()
        {
            var m = new Marker();
            m.SetMediator(this);
            this.markers.Add(m);
            return m;   
        }

        public void Send(Point location, Marker marker)
        {
            this.markers.Where(m => m != marker).ToList().ForEach(m => m.ReceiveLocation(location));
        }
    }
}
