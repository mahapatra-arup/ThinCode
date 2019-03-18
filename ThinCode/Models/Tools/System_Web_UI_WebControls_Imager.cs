using System;
using System.Web.UI.WebControls;

namespace ThinCode
{
    public static class System_Web_UI_WebControls_Imager
    {
        #region ------------Web Control Image Tools-------------
        public static void GetFileUploadImageHeightWidth(FileUpload FileUpload1, out double height, out double width)
        {
            height = 0; width = 0;

            System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
             height = img.PhysicalDimension.Height;
             width = img.PhysicalDimension.Width;
            }

        public static Image byteArrayToWebImage(this byte[] bytesArr)
        {
            try
            {
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                string base64String = Convert.ToBase64String(bytesArr, 0, bytesArr.Length);
                img.ImageUrl = "data:image/png;base64," + base64String;
                return img;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static byte[] ImageControlToByte(ref Image img)
        {
            byte[] User_Image = null;
            try
            {
                //read from image control
                if (img.ImageUrl != null)
                {
                    string convert = (img.ImageUrl).Replace("data:image/png;base64,", String.Empty);
                    User_Image = Convert.FromBase64String(convert);

                    return User_Image;

                    // or
                    //var imageParts = (ImageOfUser.ImageUrl).Split(',').ToList<string>();
                    ////Exclude the header from base64 by taking second element in List.
                    //byte[] Image = Convert.FromBase64String(imageParts[1]);
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
       
        #endregion
    }
}