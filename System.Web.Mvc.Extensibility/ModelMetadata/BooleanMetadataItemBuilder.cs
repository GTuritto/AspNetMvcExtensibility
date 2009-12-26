#region Copyright
// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    /// <summary>
    /// Defines a class to fluently configure metadata of a <seealso cref="bool"/> type.
    /// </summary>
    public class BooleanMetadataItemBuilder : ModelMetadataItemBuilderBase<BooleanMetadataItem, BooleanMetadataItemBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanMetadataItemBuilder"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public BooleanMetadataItemBuilder(BooleanMetadataItem item) : base(item)
        {
        }
    }
}