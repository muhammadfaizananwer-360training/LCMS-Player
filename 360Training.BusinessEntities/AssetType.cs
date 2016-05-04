using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    /*
    public enum AssetType
    {
        	Image = 1,
        	Document = 2,
        	FlashObject = 3,
        	AudioClip = 4,
        	MovieClip = 5,
            Text = 6
    }
    */

    /// <summary>
    /// This class provides type properties for asset
    /// </summary>
    public class AssetType
    {
        public const string Image = "Image";
        public const string Document = "Document";
        public const string FlashObject = "Flash Object";
        public const string AudioClip = "Audio Clip";
        public const string MovieClip = "Movie Clip";
        public const string Text = "Text";
        //LCMS-11217
        public const string Affidavit = "Affidavit";
    }
}
