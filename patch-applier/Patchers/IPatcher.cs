using System.IO;

namespace patch_manager.Patchers
{
    public interface IPatcher
    {
        void Patch(Stream targetStream);
    }
}
