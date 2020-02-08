using System.IO;

namespace ips_patch_manager.Patchers
{
    public interface IPatcher
    {
        void Patch(Stream targetStream);
    }
}
