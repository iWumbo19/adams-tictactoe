using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace SassyTicTacToe
{
    class ImageControls
    {
        /// <summary>
        /// Used to grab image data from properties. Helper method ByteToImage
        /// </summary>
        /// <param name="square"></param>
        /// <returns>the ImageSource object needed to display Tiles based on Square passed in</returns>
        internal static ImageSource RetrieveImage(Square square)
        {
            if (square.Tile == null) { return ByteToImage(Properties.Resources.Empty); }
            else
            {
                if (square.Tile.TileType == TileType.X) { return ByteToImage(Properties.Resources.X); }
                else { return ByteToImage(Properties.Resources.O); }
            }
        }

        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
    }
}
