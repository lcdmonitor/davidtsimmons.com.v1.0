using System;

namespace DavidSimmons.Core.Dependency
{
    //todo: implement appropriate unity extions so we can use this attribute rather 
    //todo: than Unity dependency attribute limiting explson of unity references
    /// <summary>
    /// Marker Attribute for Properties to Be Resolved
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ResolveAttribute : Attribute
    {
        public string DependencyDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyDescription">Description of property to be resolved</param>
        public ResolveAttribute(string dependencyDescription)
        {
            this.DependencyDescription = dependencyDescription;
        }
    }
}
