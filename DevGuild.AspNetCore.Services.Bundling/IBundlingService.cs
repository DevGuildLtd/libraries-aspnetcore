using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace DevGuild.AspNetCore.Services.Bundling
{
    /// <summary>
    /// Defines interface of the bundling service.
    /// </summary>
    public interface IBundlingService
    {
        /// <summary>
        /// Renders the styles bundle or its content with version timestamps.
        /// </summary>
        /// <param name="bundlePath">The bundle path.</param>
        /// <returns>HTML-code that contain either reference to the bundle or to its components.</returns>
        Task<IHtmlContent> RenderStylesBundleAsync(String bundlePath);

        /// <summary>
        /// Renders the scripts bundle or its content with version timestamps.
        /// </summary>
        /// <param name="bundlePath">The bundle path.</param>
        /// <returns>HTML-code that contain either reference to the bundle or to its components.</returns>
        Task<IHtmlContent> RenderScriptsBundleAsync(String bundlePath);

        ///// <summary>
        ///// Renders the static JavaScript file reference with version timestamp.
        ///// </summary>
        ///// <param name="filePath">The file path.</param>
        ///// <returns>HTML-code that contains timestamped reference to the file.</returns>
        //IHtmlContent RenderScript(String filePath);

        ///// <summary>
        ///// Renders the static Stylesheet file reference with version timestamp.
        ///// </summary>
        ///// <param name="filePath">The file path.</param>
        ///// <returns>HTML-code that contains timestamped reference to the file.</returns>
        //IHtmlContent RenderStyle(String filePath);
    }

}
