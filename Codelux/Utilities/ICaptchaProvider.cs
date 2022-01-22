using System.Drawing;

namespace Codelux.Utilities
{
    public interface ICaptchaProvider
    {
        string CaptchaText { get; }
        Bitmap CreateCaptchaImage();
    }
}
