// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 21/02/2023     \\

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Dirfile-Creator")]

namespace Dirfile_lib.Core
{
    /// <summary>
    /// Contains file extensions, not all of them
    /// </summary>
    internal class DirfileExtensions
    {

    #region Audio file extensions

        /// <summary>
        /// File extensions related to Audio.
        /// </summary>
        public enum Audio
        {
            /// <summary>
            /// AIF audio file.
            /// </summary>
            AIF,

            /// <summary>
            /// CD autio track file.
            /// </summary>
            CDA,

            /// <summary>
            /// MID audio file.
            /// </summary>
            MID,

            /// <summary>
            /// MIDI audio file.
            /// </summary>
            MIDI,

            /// <summary>
            /// MP3 audio file.
            /// </summary>
            MP3,

            /// <summary>
            /// MPEG-2 audtio file.
            /// </summary>
            MPA,

            /// <summary>
            /// Ogg Vorbis audio file.
            /// </summary>
            OGG,

            /// <summary>
            /// Wav file.
            /// </summary>
            WAV,

            /// <summary>
            /// WMA audio file.
            /// </summary>
            WMA,

            /// <summary>
            /// Windows Media Player playlist.
            /// </summary>
            WPL,
        }


        #endregion

    #region Compressed file extensions

        /// <summary>
        /// File extensions related to Compressing.
        /// </summary>
        public enum Compressed
        {
            /// <summary>
            /// 7-Zip compressed file.
            /// </summary>
            SevenZip,

            /// <summary>
            /// ARJ compressed file.
            /// </summary>
            ARJ,

            /// <summary>
            /// Debian software package file.
            /// </summary>
            DEB,

            /// <summary>
            /// Package file.
            /// </summary>
            PKG,

            /// <summary>
            /// RAR file.
            /// </summary>
            RAR,

            /// <summary>
            /// Red Hat Package Manager file.
            /// </summary>
            RPM,

            /// <summary>
            /// Tarball compressed file.
            /// </summary>
            TARGZ,

            /// <summary>
            /// Z compressed file.
            /// </summary>
            Z,

            /// <summary>
            /// Zip compressed file.
            /// </summary>
            ZIP,
        }

    #endregion

    #region Disc and media file extensions

        /// <summary>
        /// File extensions related to Discs and media.
        /// </summary>
        public enum DiscMedia
        {
            /// <summary>
            /// Binary disc image.
            /// </summary>
            BIN,

            /// <summary>
            /// MacOS X disk image.
            /// </summary>
            DMG,

            /// <summary>
            /// ISO disc image.
            /// </summary>
            ISO,

            /// <summary>
            /// Toast disc image.
            /// </summary>
            TOAST,

            /// <summary>
            /// Virtual CD file format.
            /// </summary>
            VCD
        }

    #endregion

    #region Data and database file extensions

        /// <summary>
        /// File extensions related to data.
        /// </summary>
        public enum Data
        {
            /// <summary>
            /// Comma separated value file.
            /// </summary>
            CSV,

            /// <summary>
            /// Data file.
            /// </summary>
            DAT,

            /// <summary>
            /// Database file.
            /// </summary>
            LOG,

            /// <summary>
            /// Database file.
            /// </summary>
            SAV,

            /// <summary>
            /// Linux/Unix tarball file archive.
            /// </summary>
            TAR,

            /// <summary>
            /// XML file.
            /// </summary>
            XML
        }

        /// <summary>
        /// File extensions related to database.
        /// </summary>
        public enum Database
        {
            /// <summary>
            /// Database file.
            /// </summary>
            DB,

            /// <summary>
            /// Database file.
            /// </summary>
            DBF,

            /// <summary>
            /// Microsoft Access database file.
            /// </summary>
            MDB,

            /// <summary>
            /// SQL database file.
            /// </summary>
            SQL,
        }

    #endregion

    #region E-mail file extension

        /// <summary>
        /// File extensions related to Email.
        /// </summary>
        public enum Email
        {
            /// <summary>
            /// Outlook Express e=mail message file.
            /// </summary>
            EMAIL,

            /// <summary>
            /// E=mail message file from multiple e-mail clients.
            /// </summary>
            EML,

            /// <summary>
            /// Apple Mail e-mail file.
            /// </summary>
            EMLX,

            /// <summary>
            /// Microsoft Outlook e-mail message file.
            /// </summary>
            MSG,

            /// <summary>
            /// Microsoft Outlook e-mail template file.
            /// </summary>
            OFT,

            /// <summary>
            /// Microsoft Outlook offline e-mail storage file.
            /// </summary>
            OST,

            /// <summary>
            /// Microsoft Outlook e-mail storage file.
            /// </summary>
            PST,

            /// <summary>
            /// E-mail contact file.
            /// </summary>
            VCF
        }

    #endregion

    #region Executable file extensions

        /// <summary>
        /// File extensions related to Executables.
        /// </summary>
        public enum Executable
        {
            /// <summary>
            /// Android package file.
            /// </summary>
            APK,

            /// <summary>
            /// Batch file.
            /// </summary>
            BAT,

            /// <summary>
            /// Binary file.
            /// </summary>
            BIN,

            /// <summary>
            /// MS-DOS command file.
            /// </summary>
            COM,

            /// <summary>
            /// Executable file.
            /// </summary>
            EXE,

            /// <summary>
            /// Windows gadget.
            /// </summary>
            GADGET,

            /// <summary>
            /// Java Archive file.
            /// </summary>
            JAR,

            /// <summary>
            /// Windows installer package.
            /// </summary>
            MSI,

            /// <summary>
            /// Python file.
            /// </summary>
            PY,

            /// <summary>
            /// Windows Script File.
            /// </summary>
            WSF
        }

    #endregion

    #region Font file extensions

        /// <summary>
        /// File extensions related to Fonts.
        /// </summary>
        public enum Fonts
        {
            /// <summary>
            /// Windows font file.
            /// </summary>
            FNT,

            /// <summary>
            /// Generic font file.
            /// </summary>
            FON,

            /// <summary>
            /// Open type font file.
            /// </summary>
            OTF,

            /// <summary>
            /// TrueType font file.
            /// </summary>
            TTF
        }

    #endregion

    #region Image file extensions

        /// <summary>
        /// File extensions related to Images.
        /// </summary>
        public enum Image
        {
            /// <summary>
            /// Adobe Illustrator file.
            /// </summary>
            AI,

            /// <summary>
            /// Bitmap image.
            /// </summary>
            BMP,

            /// <summary>
            /// GIF image.
            /// </summary>
            GIF,

            /// <summary>
            /// Icon file.
            /// </summary>
            ICO,

            /// <summary>
            /// JPEG image.
            /// </summary>
            JPG,

            /// <summary>
            /// JPEG image.
            /// </summary>
            JPEG,

            /// <summary>
            /// PNG image.
            /// </summary>
            PNG,

            /// <summary>
            /// PostScript file.
            /// </summary>
            PS,

            /// <summary>
            /// PSD image.
            /// </summary>
            PSD,

            /// <summary>
            /// Scalable Vector Graphics file.
            /// </summary>
            SVG,

            /// <summary>
            /// TIFF image.
            /// </summary>
            TIF,

            /// <summary>
            /// TIFF image.
            /// </summary>
            TIFF,

            /// <summary>
            /// WebP image.
            /// </summary>
            WEBP
        }

    #endregion

    #region Presentation file extensions

        /// <summary>
        /// File extensions related to Presentation.
        /// </summary>
        public enum Presentation
        {
            /// <summary>
            /// Keynote presentation.
            /// </summary>
            KEY,

            /// <summary>
            /// OpenOfficeImpress presentation file.
            /// </summary>
            ODP,

            /// <summary>
            /// PowerPoint slide show.
            /// </summary>
            PPS,

            /// <summary>
            /// PowerPoint presentation.
            /// </summary>
            PPT,

            /// <summary>
            /// PowerPoint Open XML presentation.
            /// </summary>
            PPTX
        }

    #endregion

    #region Programming file extensions

        /// <summary>
        /// File extensions related to Programming.
        /// </summary>
        public enum Programming
        {
            /// <summary>
            /// C and C++ source code file.
            /// </summary>
            C,

            /// <summary>
            /// Perl script file.
            /// </summary>
            CGI,

            /// <summary>
            /// Perl script file.
            /// </summary>
            PL,

            /// <summary>
            /// Java class file.
            /// </summary>
            CLASS,

            /// <summary>
            /// C++ source code file.
            /// </summary>
            CPP,

            /// <summary>
            /// C# source code file.
            /// </summary>
            CS,

            /// <summary>
            /// C, C++ and Objective-C header files.
            /// </summary>
            H,

            /// <summary>
            /// Java Source code file.
            /// </summary>
            JAVA,

            /// <summary>
            /// JavaScript file.
            /// </summary>
            JS,

            /// <summary>
            /// PHP script file.
            /// </summary>
            PHP,

            /// <summary>
            /// Python script file.
            /// </summary>
            PY,

            /// <summary>
            /// Bash shell script.
            /// </summary>
            SH,

            /// <summary>
            /// Swift source code file.
            /// </summary>
            SWIFT,

            /// <summary>
            /// VisualBasic file.
            /// </summary>
            VB
        }

    #endregion

    #region Spreadsheet file extensions

        /// <summary>
        /// File extensions related to Spreadsheet.
        /// </summary>
        public enum Spreadsheet
        {
            /// <summary>
            /// OpenOffice Calc spreadsheet file.
            /// </summary>
            ODS,

            /// <summary>
            /// Microsoft Excel.
            /// </summary>
            XLS,

            /// <summary>
            /// Microsoft Excel file with macros.
            /// </summary>
            XLSM,

            /// <summary>
            /// Microsoft Excel Open XML spreadsheet file.
            /// </summary>
            XLSX
        }

    #endregion

    #region System file extensions

        /// <summary>
        /// File extensions related to System.
        /// </summary>
        public enum System
        {
            /// <summary>
            /// Backup file.
            /// </summary>
            BAK,

            /// <summary>
            /// Windows Cabinet file.
            /// </summary>
            CAB,

            /// <summary>
            /// Configuration file.
            /// </summary>
            CFG,

            /// <summary>
            /// Windows Control panel file.
            /// </summary>
            CPL,

            /// <summary>
            /// Windows cursor file.
            /// </summary>
            CUR,

            /// <summary>
            /// DLL file.
            /// </summary>
            DLL,

            /// <summary>
            /// Dump file.
            /// </summary>
            DMP,

            /// <summary>
            /// Device driver file.
            /// </summary>
            DRV,

            /// <summary>
            /// MacOS X icos resource file.
            /// </summary>
            ICNS,

            /// <summary>
            /// Initialization file.
            /// </summary>
            INI,

            /// <summary>
            /// Windows shortcut file.
            /// </summary>
            INK,

            /// <summary>
            /// Windows system file.
            /// </summary>
            SYS,

            /// <summary>
            /// Temporary file.
            /// </summary>
            TMP
        }

    #endregion

    #region Video file extensions

        /// <summary>
        /// File extensions related to Video.
        /// </summary>
        public enum Video
        {
            /// <summary>
            /// 3GPP2 multimedia file.
            /// </summary>
            THREEG2,

            /// <summary>
            /// 3GPP multimedia file.
            /// </summary>
            THREEGP,

            /// <summary>
            /// AVI file.
            /// </summary>
            AVI,

            /// <summary>
            /// Adobe flash file.
            /// </summary>
            FLV,

            /// <summary>
            /// H.264 video file.
            /// </summary>
            H264,

            /// <summary>
            /// Apple MP4 video file.
            /// </summary>
            M4V,

            /// <summary>
            /// Matroska Multimedia Container.
            /// </summary>
            MKV,

            /// <summary>
            /// Apple QuickTime movie file.
            /// </summary>
            MOV,

            /// <summary>
            /// MPEG4 video file.
            /// </summary>
            MP4,

            /// <summary>
            /// MPEG video file.
            /// </summary>
            MPG,

            /// <summary>
            /// MPEG video file.
            /// </summary>
            MPEG,

            /// <summary>
            /// RealMedia file.
            /// </summary>
            RM,

            /// <summary>
            /// Shockwave flash file.
            /// </summary>
            SWF,

            /// <summary>
            /// DVD Video Object.
            /// </summary>
            VOB,

            /// <summary>
            /// WebM video file.
            /// </summary>
            WEBM,

            /// <summary>
            /// Windows Media Video File.
            /// </summary>
            WMV
        }

    #endregion

    #region Word and text file extensions

        /// <summary>
        /// File extensions related to text.
        /// </summary>
        public enum Text
        {
            /// <summary>
            /// Microsoft Word file.
            /// </summary>
            DOC,

            /// <summary>
            /// Microsoft Word file.
            /// </summary>
            DOCX,

            /// <summary>
            /// OpenOffice Writer document file.
            /// </summary>
            ODT,

            /// <summary>
            /// PDF file.
            /// </summary>
            PDF,

            /// <summary>
            /// Ritch Text Format.
            /// </summary>
            RTF,

            /// <summary>
            /// LaTeX document file.
            /// </summary>
            TEX,

            /// <summary>
            /// Plaint text file.
            /// </summary>
            TXT,

            /// <summary>
            /// WordPerfect document.
            /// </summary>
            WPD
        }

    #endregion

    #region Other extensions

        /// <summary>
        /// Other file extensions.
        /// </summary>
        public enum Other
        {
            /// <summary>
            /// Active Server Page file.
            /// </summary>
            ASP,

            /// <summary>
            /// Active Server Page file.
            /// </summary>
            ASPX,

            /// <summary>
            /// Internet security cerificate.
            /// </summary>
            CER,

            /// <summary>
            /// ColdFusion Markup file.
            /// </summary>
            CFM,

            /// <summary>
            /// Cascading Style sheet file.
            /// </summary>
            CSS,

            /// <summary>
            /// HTML file.
            /// </summary>
            HTM,

            /// <summary>
            /// HTML file.
            /// </summary>
            HTML,

            /// <summary>
            /// Java Server Page file.
            /// </summary>
            JSP,

            /// <summary>
            /// Partially downloaded file.
            /// </summary>
            PART,

            /// <summary>
            /// RSS file.
            /// </summary>
            RSS,

            /// <summary>
            /// XHTML file.
            /// </summary>
            XHTML
        }

    #endregion

    }
}