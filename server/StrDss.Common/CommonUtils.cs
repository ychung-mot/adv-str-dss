using NetTopologySuite.Geometries;

namespace StrDss.Common
{
    public static class CommonUtils
    {
        public static Coordinate[] PointsToCoordinate(double[][] points)
        {
            var coordinates = new List<Coordinate>();
            foreach (var point in points)
            {
                coordinates.Add(new Coordinate(point[0], point[1]));
            }

            return coordinates.ToArray();
        }
    }
}
