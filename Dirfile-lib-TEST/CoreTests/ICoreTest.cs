// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using System.Reflection;
using PropConsts = Dirfile_lib.Core.Constants.Texts.DirfileProps;

namespace Dirfile_lib_TEST.CoreTests
{
    /// <summary>
    /// Interface for Comparing properties (only for DirectoryInfo, resp. Director and FileInfo, resp. Filer)
    /// </summary>
    internal interface ICoreTest
    {
        /// <summary>
        /// Compares properties of expected values (Dictionary) with <see cref="Filer"/> or <see cref="Director"/> class.
        /// </summary>
        /// <typeparam name="U">Dictionary of expected values.</typeparam>
        /// <typeparam name="V"><see cref="Filer"/> or <see cref="Director"/> class.</typeparam>
        /// <param name="expectedDictionary">Dictionary of expected values.</param>
        /// <param name="actual">Actual values.</param>
        /// <param name="exists">True if file or directory exist, otherwise false.</param>
        /// <returns>True if properties are set correctly, otherwise false.</returns>
        private static bool CompareDictionary<U, V>(U expectedDictionary, V actual, bool exists)
            where U : IDictionary<string, object?>
        {
            int setProperties = 0;

            if (actual == null)
                return false;

            PropertyInfo[] actualProperties = actual.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (var expectedPropertyByPropertyName in expectedDictionary)
            {
                foreach (var actualPropertyByPropertyName in actualProperties)
                {
                    if (expectedPropertyByPropertyName.Key == actualPropertyByPropertyName.Name && expectedPropertyByPropertyName.Key == PropConsts.Directory)
                    {
                        var expectedName = ((Director)expectedPropertyByPropertyName.Value!).FullName;

                        if (actualPropertyByPropertyName.GetValue(actual) is not Director actValue)
                            return false;

                        if (expectedName != actValue.FullName)
                            return false;

                        setProperties++;
                    }
                    else if (expectedPropertyByPropertyName.Key == actualPropertyByPropertyName.Name)
                    {
                        if (expectedPropertyByPropertyName.Key == PropConsts.Length && !exists)
                        {
                            var fileLength = actualPropertyByPropertyName.GetValue(actual, null);
                            fileLength ??= string.Empty;

                            if (fileLength.ToString() != "0")
                                return false;

                            setProperties++;
                            break;
                        }

                        var expectedProperty = expectedPropertyByPropertyName.Value;
                        var actualProperty = actualPropertyByPropertyName.GetValue(actual);

                        expectedProperty ??= string.Empty;
                        actualProperty ??= string.Empty;

                        if (expectedProperty.ToString() != actualProperty.ToString())
                            return false;

                        setProperties++;
                    }
                }
            }

            if (setProperties != expectedDictionary.Count)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Compares properties of expected values (<see cref="FileInfo"/> or <see cref="DirectoryInfo"/>) with 
        ///     <see cref="Filer"/> or <see cref="Director"/> class.
        /// </summary>
        /// <typeparam name="T"><see cref="FileInfo"/> or <see cref="DirectoryInfo"/> class.</typeparam>
        /// <typeparam name="V"><see cref="Filer"/> or <see cref="Director"/> class.</typeparam>
        /// <param name="expectedInfo">Expected values.</param>
        /// <param name="actual">Actual values.</param>
        /// <param name="exists">True if file or directory exist, otherwise false.</param>
        /// <returns>True if properties are set correctly, otherwise false.</returns>
        private static bool CompareInfo<T, V>(T expectedInfo, V actual, bool exists)
            where T : FileSystemInfo
        {
            int setProperties = 0;
            if (typeof(T) != typeof(FileInfo) && typeof(V) != typeof(Filer) &&
                        typeof(T) != typeof(DirectoryInfo) && typeof(V) != typeof(Director) ||
                        expectedInfo == null || actual == null)
            {
                return false;
            }

            PropertyInfo[] actProperties = actual.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] expProperties = expectedInfo.GetType().GetProperties();

            // Compare everything
            foreach (var expProperty in expProperties)
            {
                foreach (var actProperty in actProperties)
                {
                    if (expProperty.Name == actProperty.Name && 
                        (expProperty.Name == PropConsts.Directory || expProperty.Name == PropConsts.Parent || expProperty.Name == PropConsts.Root))
                    {
                        var expected = expProperty.GetValue(expectedInfo);

                        if (actProperty.GetValue(actual) is not Director actValue || expected == null)
                            return false;

                        var res = ((DirectoryInfo)expected).FullName;
                        if (res != actValue.FullName)
                            return false;

                        setProperties++;
                    }
                    else if (expProperty.Name == actProperty.Name && expProperty.Name != PropConsts.Directory)
                    {
                        if (expProperty.Name == PropConsts.Length && !exists)
                        {
                            var len = actProperty.GetValue(actual, null);
                            len ??= string.Empty;

                            if (len.ToString() != "0")
                                return false;

                            setProperties++;
                            break;
                        }

                        var expValue = expProperty.GetValue(expectedInfo, null);
                        var actValue = actProperty.GetValue(actual);

                        expValue ??= string.Empty;
                        actValue ??= string.Empty;

                        if (expValue.ToString() != actValue.ToString())
                            return false;

                        setProperties++;
                    }
                }
            }

            if (setProperties != expProperties.Length)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Compares properties of <see cref="FileInfo"/> and <see cref="Filer"/> classes.
        /// </summary>
        /// <param name="expected">FileInfo properties (expected properties).</param>
        /// <param name="actual">Filer properties (actual properties).</param>
        /// <param name="exists">True if exists, otherwise false.</param>
        /// <returns>True if they're the same, otherwise returns false.</returns>
        static bool CompareProperties<T, U, V>(T expectedInfo, U expectedDictionary, V actual, bool exists)
            where T : FileSystemInfo
            where U : IDictionary<string, object?>
        {
            if (expectedInfo.FullName == Directory.GetCurrentDirectory() + "\\DirFileTest") // Use dictionary as expected results
                return CompareDictionary(expectedDictionary, actual, exists);
            else // Use FileInfo or DirectoryInfo as expected results
                return CompareInfo(expectedInfo, actual, exists);
        }
    }
}
