using EmptyFlow.SciterAPI.Enums;
using System.Runtime.InteropServices;

namespace EmptyFlow.SciterAPI.Structs {

    public delegate GraphInResult GraphicsImageCreate ( IntPtr poutImg, uint width, uint height, bool withAlpha );

    public delegate GraphInResult GraphicsImageCreateFromPixmap ( IntPtr poutImg, uint pixmapWidth, uint pixmapHeight, SciterPimapFormat pixmap_format, byte[] pixmap );
    public delegate GraphInResult GraphicsImageAddRef ( IntPtr himg );
    public delegate GraphInResult GraphicsImageRelease ( IntPtr himg );
    public delegate GraphInResult GraphicsImageGetInfo ( IntPtr himg, out uint width, out uint height, out bool usesAlpha );
    public delegate GraphInResult GraphicsImageClear ( IntPtr himg, uint byColor );
    public delegate GraphInResult GraphicsImageLoad ( byte[] bytes, uint numBytes, IntPtr poutImg ); // 
    public delegate GraphInResult GraphicsImageSave ( IntPtr himg, IntPtr pfn, IntPtr prm, SciterImageEncoding encoding, uint quality /* if webp or jpeg:, 10 - 100 */ );
    public delegate uint GraphicsRgba ( uint red, uint green, uint blue, uint alpha /*=255*/ );
    public delegate GraphInResult GraphicsGCreate ( IntPtr himg, IntPtr poutGfx );
    public delegate GraphInResult GraphicsGAddRef ( IntPtr gfx );
    public delegate GraphInResult GraphicsGRelease ( IntPtr gfx );

    public delegate GraphInResult GraphicsGLine ( IntPtr hgfx, float x1, float y1, float x2, float y2 );
    public delegate GraphInResult GraphicsGRectangle ( IntPtr hgfx, float x1, float y1, float x2, float y2 );
    public delegate GraphInResult GraphicsGRoundedRectangle ( IntPtr hgfx, float x1, float y1, float x2, float y2, float[] radii8 ); // radii8 is SC_DIM[8] - four rx/ry pairs.
    public delegate GraphInResult GraphicsGEllipse ( IntPtr hgfx, float x, float y, float rx, float ry );
    public delegate GraphInResult GraphicsGArc ( IntPtr hgfx, float x, float y, float rx, float ry, float start, float sweep );
    public delegate GraphInResult GraphicsGStar ( IntPtr hgfx, float x, float y, float r1, float r2, float start, uint rays );
    public delegate GraphInResult GraphicsGPolygon ( IntPtr hgfx, float[] xy, uint numPoints );
    public delegate GraphInResult GraphicsGPolyline ( IntPtr hgfx, float[] xy, uint numPoints );

    public delegate GraphInResult GraphicsPathCreate ( IntPtr path );
    public delegate GraphInResult GraphicsPathAddRef ( IntPtr path );
    public delegate GraphInResult GraphicsPathRelease ( IntPtr path );
    public delegate GraphInResult GraphicsPathMoveTo ( IntPtr path, float x, float y, bool relative );
    public delegate GraphInResult GraphicsPathLineTo ( IntPtr path, float x, float y, bool relative );
    public delegate GraphInResult GraphicsPathArcTo ( IntPtr path, float x, float y, float angle, float rx, float ry, bool isLargeArc, bool clockwise, bool relative );
    public delegate GraphInResult GraphicsPathQuadraticCurveTo ( IntPtr path, float xc, float yc, float x, float y, bool relative );
    public delegate GraphInResult GraphicsPathBezierCurveTo ( IntPtr path, float xc1, float yc1, float xc2, float yc2, float x, float y, bool relative );
    public delegate GraphInResult GraphicsPathClosePath ( IntPtr path );
    public delegate GraphInResult GraphicsGDrawPath ( IntPtr hgfx, IntPtr path, DrawPathMode dpm );
    public delegate GraphInResult GraphicsGRotate ( IntPtr hgfx, float radians, float[] cx, float[] cy );
    public delegate GraphInResult GraphicsGTranslate ( IntPtr hgfx, float cx, float cy );
    public delegate GraphInResult GraphicsGScale ( IntPtr hgfx, float x, float y );
    public delegate GraphInResult GraphicsGSkew ( IntPtr hgfx, float dx, float dy );
    public delegate GraphInResult GraphicsGTransform ( IntPtr hgfx, float m11, float m12, float m21, float m22, float dx, float dy );
    public delegate GraphInResult GraphicsGStateSave ( IntPtr hgfx );
    public delegate GraphInResult GraphicsGStateRestore ( IntPtr hgfx );
    public delegate GraphInResult GraphicsGLineWidth ( IntPtr hgfx, float width );
    public delegate GraphInResult GraphicsGLineJoin ( IntPtr hgfx, SciterLineJoinType type );
    public delegate GraphInResult GraphicsGLineCap ( IntPtr hgfx, SciterLineCapType type );
    public delegate GraphInResult GraphicsGLineColor ( IntPtr hgfx, uint color );
    public delegate GraphInResult GraphicsGFillColor ( IntPtr hgfx, uint color );
    public delegate GraphInResult GraphicsGLineGradientLinear ( IntPtr hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops );
    public delegate GraphInResult GraphicsGFillGradientLinear ( IntPtr hgfx, float x1, float y1, float x2, float y2, ColorStop[] stops, uint nstops );
    public delegate GraphInResult GraphicsGLineGradientRadial ( IntPtr hgfx, float x, float y, float rx, float ry, ColorStop[] stops, uint nstops );
    public delegate GraphInResult GraphicsGFillGradientRadial ( IntPtr hgfx, float x, float y, float rx, float ry, ColorStop[] stops, uint nstops );
    public delegate GraphInResult GraphicsGFillMode ( IntPtr hgfx, bool evenOdd );
    public delegate GraphInResult GraphicsTextCreateForElement ( out nint ptext, [MarshalAs ( UnmanagedType.LPWStr )] string text, uint textLength, IntPtr he, [MarshalAs ( UnmanagedType.LPWStr )] string classNameOrNull );
    public delegate GraphInResult GraphicsTextCreateForElementAndStyle ( out nint ptext, [MarshalAs ( UnmanagedType.LPWStr )] string text, uint textLength, IntPtr he, [MarshalAs ( UnmanagedType.LPWStr )] string style, uint styleLength );
    public delegate GraphInResult GraphicsTextAddRef ( IntPtr path );
    public delegate GraphInResult GraphicsTextRelease ( IntPtr path );
    public delegate GraphInResult GraphicsTextGetMetrics ( IntPtr text, out float minWidth, out float maxWidth, out float height, out float ascent, out float descent, out float nLines );
    public delegate GraphInResult GraphicsTextSetBox ( IntPtr text, float width, float height );
    public delegate GraphInResult GraphicsGDrawText ( IntPtr hgfx, IntPtr text, float px, float py, uint position );
    public delegate GraphInResult GraphicsGDrawImage ( IntPtr hgfx, IntPtr himg, float x, float y,
        float w /*= 0 */, float h /*= 0 */,
        uint ix /*= 0 */, uint iy /*= 0 */, uint iw /*= 0 */, uint ih /*= 0 */,
        float opacity/*= 0, if provided is in 0.0 .. 1.0 */
        );
    public delegate GraphInResult GraphicsGWorldToScreen ( IntPtr hgfx, ref float inoutX, ref float inoutY );
    public delegate GraphInResult GraphicsGScreenToWorld ( IntPtr hgfx, ref float inoutX, ref float inoutY );
    public delegate GraphInResult GraphicsGPushClipBox ( IntPtr hgfx, float x1, float y1, float x2, float y2, float opacity /* =1.f */);
    public delegate GraphInResult GraphicsGPushClipPath ( IntPtr hgfx, IntPtr hpath, float opacity /* =1.f */);
    public delegate GraphInResult GraphicsGPopClip ( IntPtr hgfx );
    public delegate void GraphicsImagePainter ( IntPtr prm, IntPtr hgfx, uint width, uint height );
    public delegate GraphInResult GraphicsImagePaint ( IntPtr himg, GraphicsImagePainter pPainter, IntPtr prm );

    public delegate GraphInResult GraphicsVWrapGfx ( IntPtr hgfx, SciterValue toValue );
    public delegate GraphInResult GraphicsVWrapImage ( IntPtr himg, SciterValue toValue );
    public delegate GraphInResult GraphicsVWrapPath ( IntPtr hpath, SciterValue toValue );
    public delegate GraphInResult GraphicsVWrapText ( IntPtr htext, SciterValue toValue );

    public delegate GraphInResult GraphicsVUnWrapGfx ( SciterValue toValue, IntPtr hgfx );
    public delegate GraphInResult GraphicsVUnWrapImage ( SciterValue toValue, IntPtr himg );
    public delegate GraphInResult GraphicsVUnWrapPath ( SciterValue toValue, IntPtr hpath );
    public delegate GraphInResult GraphicsVUnWrapText ( SciterValue toValue, IntPtr htext );
    public delegate GraphInResult GraphicsGFlush ( IntPtr hgfx );
    public delegate GraphInResult GraphicsImageCreateFromElement ( IntPtr poutImg, IntPtr domElement );
    public delegate GraphInResult GraphicsGGetNativeDC ( nint hgfx, uint nativeDcType, IntPtr pDC );


    [StructLayout ( LayoutKind.Sequential )]
    public readonly struct GraphicsApiStruct {

        public readonly GraphicsImageCreate ImageCreate;

        /// <summary>
        /// construct image from B[n+0],G[n+1],R[n+2],A[n+3] data.
        /// size of pixmap data is pixmapWidth*pixmapHeight*4,
        /// pixmap_format is SCITER_PIXMAP_FORMAT above.
        /// </summary>
        public readonly GraphicsImageCreateFromPixmap ImageCreateFromPixmap;
        public readonly GraphicsImageAddRef ImageAddRef;
        public readonly GraphicsImageRelease ImageRelease;
        public readonly GraphicsImageGetInfo ImageGetInfo;
        public readonly GraphicsImageClear ImageClear;

        /// <summary>
        /// load png/jpeg/etc. image from stream of bytes.
        /// </summary>
        public readonly GraphicsImageLoad ImageLoad;
        public readonly GraphicsImageSave ImageSave;

        public readonly GraphicsRgba RGBA;

        public readonly GraphicsGCreate gCreate;
        public readonly GraphicsGAddRef gAddRef;
        public readonly GraphicsGRelease gRelease;

        /// <summary>
        /// Draws line from x1,y1 to x2,y2 using current lineColor and lineGradient.
        /// </summary>
        public readonly GraphicsGLine gLine;

        /// <summary>
        /// Draws rectangle using current lineColor/lineGradient and fillColor/fillGradient with (optional) rounded corners.
        /// </summary>
        public readonly GraphicsGRectangle gRectangle;

        /// <summary>
        ///  Draws rounded rectangle using current lineColor/lineGradient and fillColor/fillGradient with (optional) rounded corners.
        /// </summary>
        public readonly GraphicsGRoundedRectangle gRoundedRectangle;

        /// <summary>
        /// Draws circle or ellipse using current lineColor/lineGradient and fillColor/fillGradient.
        /// </summary>
        public readonly GraphicsGEllipse gEllipse;

        /// <summary>
        /// Draws closed arc using current lineColor/lineGradient and fillColor/fillGradient.
        /// </summary>
        public readonly GraphicsGArc gArc;

        /// <summary>
        /// Draws star.
        /// </summary>
        public readonly GraphicsGStar gStar;

        /// <summary>
        /// Closed polygon.
        /// </summary>
        public readonly GraphicsGPolygon gPolygon;

        /// <summary>
        /// Polyline.
        /// </summary>
        public readonly GraphicsGPolyline gPolyline;

        public readonly GraphicsPathCreate pathCreate;
        public readonly GraphicsPathAddRef pathAddRef;
        public readonly GraphicsPathRelease pathRelease;
        public readonly GraphicsPathMoveTo pathMoveTo;
        public readonly GraphicsPathLineTo pathLineTo;
        public readonly GraphicsPathArcTo pathArcTo;
        public readonly GraphicsPathQuadraticCurveTo pathQuadraticCurveTo;
        public readonly GraphicsPathBezierCurveTo pathBezierCurveTo;
        public readonly GraphicsPathClosePath pathClosePath;
        public readonly GraphicsGDrawPath gDrawPath;

        public readonly GraphicsGRotate gRotate;

        public readonly GraphicsGTranslate gTranslate;
        public readonly GraphicsGScale gScale;
        public readonly GraphicsGSkew gSkew;
        public readonly GraphicsGTransform gTransform;
        public readonly GraphicsGStateSave gStateSave;
        public readonly GraphicsGStateRestore gStateRestore;

        /// <summary>
        /// set line width for subsequent drawings.
        /// </summary>
        public readonly GraphicsGLineWidth gLineWidth;

        public readonly GraphicsGLineJoin gLineJoin;
        public readonly GraphicsGLineCap gLineCap;

        /// <summary>
        /// SC_COLOR for solid lines/strokes.
        /// </summary>
        public readonly GraphicsGLineColor gLineColor;

        /// <summary>
        /// SC_COLOR for solid fills.
        /// </summary>
        public readonly GraphicsGFillColor gFillColor;
        public readonly GraphicsGLineGradientLinear gLineGradientLinear;

        /// <summary>
        /// setup parameters of linear gradient of fills.
        /// </summary>
        public readonly GraphicsGFillGradientLinear gFillGradientLinear;

        /// <summary>
        /// setup parameters of line gradient radial fills.
        /// </summary>
        public readonly GraphicsGLineGradientRadial gLineGradientRadial;

        /// <summary>
        /// setup parameters of gradient radial fills.
        /// </summary>
        public readonly GraphicsGFillGradientRadial gFillGradientRadial;

        public readonly GraphicsGFillMode gFillMode;

        /// <summary>
        /// Create text layout for host element.
        /// </summary>
        public readonly GraphicsTextCreateForElement textCreateForElement;

        /// <summary>
        /// Create text layout using explicit style declaration.
        /// </summary>
        public readonly GraphicsTextCreateForElementAndStyle textCreateForElementAndStyle;
        public readonly GraphicsTextAddRef textAddRef;
        public readonly GraphicsTextRelease textRelease;
        public readonly GraphicsTextGetMetrics textGetMetrics;
        public readonly GraphicsTextSetBox textSetBox;

        /// <summary>
        /// Draw text with position (1..9 on MUMPAD) at px,py
        /// Ex: gDrawText( 100,100,5) will draw text box with its center at 100,100 px.
        /// </summary>
        public readonly GraphicsGDrawText gDrawText;

        /// <summary>
        /// Draws img onto the graphics surface with current transformation applied (scale, rotation).
        /// </summary>
        public readonly GraphicsGDrawImage gDrawImage;

        public readonly GraphicsGWorldToScreen gWorldToScreen;
        public readonly GraphicsGScreenToWorld gScreenToWorld;
        public readonly GraphicsGPushClipBox gPushClipBox;
        public readonly GraphicsGPushClipPath gPushClipPath;
        /// <summary>
        /// Pop clip layer previously set by gPushClipBox or gPushClipPath.
        /// </summary>
        public readonly GraphicsGPopClip gPopClip;

        /// <summary>
        /// Paint on image using graphics.
        /// </summary>
        public readonly GraphicsImagePaint imagePaint;

        public readonly GraphicsVWrapGfx vWrapGfx;
        public readonly GraphicsVWrapImage vWrapImage;
        public readonly GraphicsVWrapPath vWrapPath;
        public readonly GraphicsVWrapText vWrapText;
        public readonly GraphicsVUnWrapGfx vUnWrapGfx;
        public readonly GraphicsVUnWrapImage vUnWrapImage;
        public readonly GraphicsVUnWrapPath vUnWrapPath;
        public readonly GraphicsVUnWrapText vUnWrapText;
        public readonly GraphicsGFlush gFlush;

        /// <summary>
        /// Grab DOM element snapshot into image.
        /// </summary>
        public readonly GraphicsImageCreateFromElement imageCreateFromElement;

        public readonly GraphicsGGetNativeDC gGetNativeDC;

    }

}
