using System.IO;

namespace patch_manager.Patchers
{
    /// <summary>
    /// Describes an object that can Patch
    /// </summary>
    public interface IPatcher
    {
        /// <summary>
        /// Describes applying a Patch to a stream
        /// </summary>
        /// <param name="targetStream"></param>
        void Patch(Stream targetStream);
    }
}
