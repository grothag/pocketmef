﻿// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.Composition.Hosting;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Internal;

#if !SILVERLIGHT
//using Microsoft.Internal.Runtime.Serialization;
#endif

namespace System.ComponentModel.Composition.Primitives
{
    /// <summary>
    ///     The exception that is thrown when an error occurs when calling methods on a
    ///     <see cref="ComposablePart"/>.
    /// </summary>
    [Serializable]
    public class ComposablePartException : Exception, ICompositionError
    {
        private readonly CompositionErrorId _id;
        private readonly ICompositionElement _element;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ComposablePartException"/> class.
        /// </summary>
        public ComposablePartException()
            : this(CompositionErrorId.Unknown, (string)null, (ICompositionElement)null, (Exception)null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ComposablePartException"/> class 
        ///     with the specified error message.
        /// </summary>
        /// <param name="message">
        ///     A <see cref="String"/> containing a message that describes the 
        ///     <see cref="ComposablePartException"/>; or <see langword="null"/> to set
        ///     the <see cref="Exception.Message"/> property to its default value.
        /// </param>
        /// <param name="element">
        ///     The <see cref="ICompositionElement"/> that is the cause of the
        ///     <see cref="ComposablePartException"/>; or <see langword="null"/> to set
        ///     the <see cref="ComposablePartException.Element"/> property to 
        ///     <see langword="null"/>.
        /// </param>
        public ComposablePartException(string message)
            : this(CompositionErrorId.Unknown, message, (ICompositionElement)null, (Exception)null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ComposablePartException"/> class 
        ///     with the specified error message and composition element that is the cause of
        ///     the exception.
        /// </summary>
        /// <param name="message">
        ///     A <see cref="String"/> containing a message that describes the 
        ///     <see cref="ComposablePartException"/>; or <see langword="null"/> to set
        ///     the <see cref="Exception.Message"/> property to its default value.
        /// </param>
        public ComposablePartException(string message, ICompositionElement element)
            : this(CompositionErrorId.Unknown, message, element, (Exception)null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ComposablePartException"/> class 
        ///     with the specified error message and exception that is the cause of the  
        ///     exception.
        /// </summary>
        /// <param name="message">
        ///     A <see cref="String"/> containing a message that describes the 
        ///     <see cref="ComposablePartException"/>; or <see langword="null"/> to set
        ///     the <see cref="Exception.Message"/> property to its default value.
        /// </param>
        /// <param name="innerException">
        ///     The <see cref="Exception"/> that is the underlying cause of the 
        ///     <see cref="ComposablePartException"/>; or <see langword="null"/> to set
        ///     the <see cref="Exception.InnerException"/> property to <see langword="null"/>.
        /// </param>
        public ComposablePartException(string message, Exception innerException)
            : this(CompositionErrorId.Unknown, message, (ICompositionElement)null, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ComposablePartException"/> class 
        ///     with the specified error message, and composition element and exception that 
        ///     are the cause of the exception.
        /// </summary>
        /// <param name="message">
        ///     A <see cref="String"/> containing a message that describes the 
        ///     <see cref="ComposablePartException"/>; or <see langword="null"/> to set
        ///     the <see cref="Exception.Message"/> property to its default value.
        /// </param>
        /// <param name="element">
        ///     The <see cref="ICompositionElement"/> that is the cause of the
        ///     <see cref="ComposablePartException"/>; or <see langword="null"/> to set
        ///     the <see cref="ComposablePartException.Element"/> property to 
        ///     <see langword="null"/>.
        /// </param>
        /// <param name="innerException">
        ///     The <see cref="Exception"/> that is the underlying cause of the 
        ///     <see cref="ComposablePartException"/>; or <see langword="null"/> to set
        ///     the <see cref="Exception.InnerException"/> property to <see langword="null"/>.
        /// </param>
        public ComposablePartException(string message, ICompositionElement element, Exception innerException)
            : this(CompositionErrorId.Unknown, message, element, innerException)
        {
        }

        internal ComposablePartException(CompositionErrorId id, string message)
            : this(id, message, (ICompositionElement)null, (Exception)null)
        {
        }

        internal ComposablePartException(CompositionErrorId id, string message, Exception exception)
            : this(id, message, (ICompositionElement)null, exception)
        {
        }

        internal ComposablePartException(CompositionErrorId id, string message, ICompositionElement element)
            : this(id, message, (ICompositionElement)element, (Exception)null)
        {
        }

        internal ComposablePartException(CompositionErrorId id, string message, ICompositionElement element, Exception innerException)
            : base(message, innerException)
        {
            _id = id;
            _element = element;
        }

#if !SILVERLIGHT

        ///// <summary>
        /////     Initializes a new instance of the <see cref="ComposablePartException"/> class 
        /////     with the specified serialization data.
        ///// </summary>
        ///// <param name="info">
        /////     The <see cref="SerializationInfo"/> that holds the serialized object data about the 
        /////     <see cref="ComposablePartException"/>.
        ///// </param>
        ///// <param name="context">
        /////     The <see cref="StreamingContext"/> that contains contextual information about the 
        /////     source or destination.
        ///// </param>
        ///// <exception cref="ArgumentNullException">
        /////     <paramref name="info"/> is <see langword="null"/>.
        ///// </exception>
        ///// <exception cref="SerializationException">
        /////     <paramref name="info"/> is missing a required value.
        ///// </exception>
        ///// <exception cref="InvalidCastException">
        /////     <paramref name="info"/> contains a value that cannot be cast to the correct type.
        ///// </exception>
        //[System.Security.SecuritySafeCritical]
        //protected ComposablePartException(SerializationInfo info, StreamingContext context)
        //    : base(info, context)
        //{
        //    _id = info.GetValue<CompositionErrorId>("Id");
        //    _element = info.GetValue<ICompositionElement>("Element");
        //}

#endif

        /// <summary>
        ///     Gets the composition element that is the cause of the exception.
        /// </summary>
        /// <value>
        ///     The <see cref="ICompositionElement"/> that is the cause of the
        ///     <see cref="ComposablePartException"/>. The default is <see langword="null"/>.
        /// </value>
        public ICompositionElement Element
        {
            get { return _element; }
        }

        CompositionErrorId ICompositionError.Id
        {
            get { return _id; }
        }

#if !SILVERLIGHT

        ///// <summary>
        /////     Gets the serialization data of the exception.
        ///// </summary>
        ///// <param name="info">
        /////     The <see cref="SerializationInfo"/> that holds the serialized object data about the 
        /////     <see cref="ComposablePartException"/>.
        ///// </param>
        ///// <param name="context">
        /////     The <see cref="StreamingContext"/> that contains contextual information about the 
        /////     source or destination.
        ///// </param>
        ///// <exception cref="ArgumentNullException">
        /////     <paramref name="info"/> is <see langword="null"/>.
        ///// </exception>
        //[System.Security.SecurityCritical]
        //public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    base.GetObjectData(info, context);

        //    info.AddValue("Id", _id);
        //    info.AddValue("Element", _element.ToSerializableElement());
        //}

#endif
    }
}