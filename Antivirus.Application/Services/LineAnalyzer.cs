using System.Text.RegularExpressions;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services;

public static class LineAnalyzer
{
    private static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        .Split(Path.DirectorySeparatorChar)!.Last()!;

    /// <summary>
    ///     Analyze line for suspicious strings.
    /// </summary>
    /// <param name="line"> Line. </param>
    /// <param name="isJs"> If file is javascript. </param>
    /// <returns> Malware type. </returns>
    public static Malware Analyze(string line, bool isJs)
    {
        if (isJs)
        {
            if (IsEvilJs(line))
            {
                return Malware.EvilJs;
            }
        }

        if (IsDllRunner(line))
        {
            return Malware.DllRunner;
        }

        if (IsRemover(line))
        {
            return Malware.Remover;
        }

        return Malware.SecureFile;
    }

    /// <summary>
    ///     Check line for evil javascript.
    /// </summary>
    /// <param name="line"> Line. </param>
    /// <returns></returns>
    public static bool IsEvilJs(string line)
    {
        return line.Contains("<script>evil_script()</script>");
    }

    /// <summary>
    ///     Check line for rm -rf %userprofile%\Documents commands.
    /// </summary>
    /// <param name="line"> Line. </param>
    /// <returns></returns>
    public static bool IsRemover(string line)
    {
        // TODO: check whats in userDirectory.

        string pattern = $@"rm -rf .*{UserProfile}.*\Documents";
        return Regex.IsMatch(line, pattern);
    }

    /// <summary>
    ///     Check line for  Rundll32 sus.dll SusEntry commands.
    /// </summary>
    /// <param name="line"> Line. </param>
    /// <returns></returns>
    public static bool IsDllRunner(string line)
    {
        return line.Contains("Rundll32 sus.dll SusEntry");
    }
}