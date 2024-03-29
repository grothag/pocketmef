﻿// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
    internal class ExportingMember
    {
        private readonly ExportDefinition _definition;
        private readonly ReflectionMember _member;
        private volatile object _cachedValue = null;

        public ExportingMember(ExportDefinition definition, ReflectionMember member)
        {
            Assumes.NotNull(definition, member);

            this._definition = definition;
            this._member = member;
        }

        public bool RequiresInstance
        {
            get { return _member.RequiresInstance; }
        }

        public ExportDefinition Definition
        {
            get { return _definition; }
        }

        public object GetExportedValue(object instance, object @lock)
        {
            this.EnsureReadable();

            if (this._cachedValue == null)
            {
                object exportedValue;
                try
                {
                    exportedValue = this._member.GetValue(instance);
                }
                catch (TargetInvocationException exception)
                {   // Member threw an exception. Avoid letting this 
                    // leak out as a 'raw' unhandled exception, instead,
                    // we'll add some context and rethrow.

                    throw new ComposablePartException(
                        CompositionErrorId.ReflectionModel_ExportThrewException,
                        String.Format(CultureInfo.CurrentCulture,
                            Strings.ReflectionModel_ExportThrewException,
                            this._member.GetDisplayName()),
                        Definition.ToElement(),
                        exception.InnerException);
                }

                lock (@lock)
                {
                    if (this._cachedValue == null)
                    {
                        this._cachedValue = exportedValue;
                    }
                }
            }

            return this._cachedValue;
        }

        private void EnsureReadable()
        {
            if (!this._member.CanRead)
            {   // Property does not have a getter

                throw new ComposablePartException(
                    CompositionErrorId.ReflectionModel_ExportNotReadable,
                    String.Format(CultureInfo.CurrentCulture, 
                        Strings.ReflectionModel_ExportNotReadable,
                        this._member.GetDisplayName()),
                    Definition.ToElement());
            }
        }
    }
}
