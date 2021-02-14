using UnityEngine;

namespace RoboticsAcademy.DataCollection
{
    /// <summary>
    /// Data writer for canvas data.
    /// </summary>
    class CanvasWriter : DataWriter
    {
        [SerializeField] Canvas canv;
        /// <summary>
        /// Get Assessment capability.
        /// </summary>
        protected override bool InitializeDataProvider() { return true; }

        /// <summary>
        /// Write assessment header.
        /// </summary>
        protected override void WriteHeader()
        {
            dataStream.WriteLine("CanvasMinX,CanvasMaxX,CanvasMinY,CanvasMaxY,CanvasHeight,CanvasWidth");

            RectTransform r = canv.GetComponent<RectTransform>();
            float minx = r.rect.min.x;
            float miny = r.rect.min.y;
            float maxx = r.rect.max.x;
            float maxy = r.rect.max.y;
            float height = r.rect.height;
            float width = r.rect.width;

            dataStream.WriteLine("{0},{1},{2},{3},{4},{5}",
                minx,
                maxx,
                miny,
                maxy,
                height,
                width);
        }
        protected override void WriteData() { }
    }
}
