// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 13/12/2022     \\

using System;

namespace Dirfile_lib.API.Context
{
    /// <summary>
    /// This is the MAIN API CLASS which si gonna be called.
    /// <para>Holds current directory, has methods for creating, deleting dirfiles,
    ///   also takes input and does the magic.</para>
    /// </summary>
    public class DirfileContext : BaseContext
    {
        /// <summary>
        ///
        /// </summary>
        public DirfileContext()
        {
            this.Extractor = new Extraction.Extractor(Extraction.SlashMode.Backward);
        }

        private string SerializePath(string str, bool pathFinder)
        {
            return string.Empty;
            //if (pathFinder)
            //    return this.Extractor.FindExistingPath(str);
            //else
            //    return this.Extractor.FindArguments();
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}