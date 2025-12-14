namespace EmptyFlow.SciterAPI.Enums {
    /// <summary>
    /// Original name SCITER_IMAGE_ENCODING.
    /// </summary>
    public enum SciterImageEncoding : uint {
        Raw, // SCITER_IMAGE_ENCODING_RAW // [a,b,g,r,a,b,g,r,...] vector
        EncodingPng, // SCITER_IMAGE_ENCODING_PNG
        EncodingJpeg,// SCITER_IMAGE_ENCODING_JPG
        EncodingWebp,// SCITER_IMAGE_ENCODING_WEBP
    }

}
