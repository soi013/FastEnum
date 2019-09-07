﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FastEnum.Internals;



namespace FastEnum
{
    /// <summary>
    /// Provides high performance utilitis for enum type.
    /// </summary>
    public static class FastEnum
    {
        #region Constants
        private const string IsDefinedTypeMismatchMessage = "The underlying type of the enum and the value must be the same type.";
        #endregion


        #region IsFlags
        /// <summary>
        /// Returns whether the <see cref="FlagsAttribute"/> is defined.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsFlags<T>()
            where T : struct, Enum
            => Cache<T>.IsFlags;
        #endregion


        #region GetUnderlyingType
        /// <summary>
        /// Returns the underlying type of the specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Type GetUnderlyingType<T>()
            where T : struct, Enum
            => Cache<T>.UnderlyingType;
        #endregion


        #region GetValues
        /// <summary>
        /// Retrieves an array of the values of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<T> GetValues<T>()
            where T : struct, Enum
            => Cache<T>.Values;
        #endregion


        #region GetNames
        /// <summary>
        /// Retrieves an array of the names of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<string> GetNames<T>()
            where T : struct, Enum
            => Cache<T>.Names;
        #endregion


        #region GetMembers
        /// <summary>
        /// Retrieves an array of the member information of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<Member<T>> GetMembers<T>()
            where T : struct, Enum
            => Cache<T>.Members;
        #endregion


        #region IsDefined
        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(T value)
            where T : struct, Enum
            => Cache<T>.MemberByValue.ContainsKey(value);


        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(sbyte value)
            where T : struct, Enum
        {
            if (Cache<T>.UnderlyingType == typeof(sbyte))
            {
                ref var @enum = ref Unsafe.As<sbyte, T>(ref value);
                return IsDefined(@enum);
            }
            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }


        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(byte value)
            where T : struct, Enum
        {
            if (Cache<T>.UnderlyingType == typeof(byte))
            {
                ref var @enum = ref Unsafe.As<byte, T>(ref value);
                return IsDefined(@enum);
            }
            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }


        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(short value)
            where T : struct, Enum
        {
            if (Cache<T>.UnderlyingType == typeof(short))
            {
                ref var @enum = ref Unsafe.As<short, T>(ref value);
                return IsDefined(@enum);
            }
            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }


        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(ushort value)
            where T : struct, Enum
        {
            if (Cache<T>.UnderlyingType == typeof(ushort))
            {
                ref var @enum = ref Unsafe.As<ushort, T>(ref value);
                return IsDefined(@enum);
            }
            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }


        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(int value)
            where T : struct, Enum
        {
            if (Cache<T>.UnderlyingType == typeof(int))
            {
                ref var @enum = ref Unsafe.As<int, T>(ref value);
                return IsDefined(@enum);
            }
            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }


        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(uint value)
            where T : struct, Enum
        {
            if (Cache<T>.UnderlyingType == typeof(uint))
            {
                ref var @enum = ref Unsafe.As<uint, T>(ref value);
                return IsDefined(@enum);
            }
            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }


        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(long value)
            where T : struct, Enum
        {
            if (Cache<T>.UnderlyingType == typeof(long))
            {
                ref var @enum = ref Unsafe.As<long, T>(ref value);
                return IsDefined(@enum);
            }
            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }


        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(ulong value)
            where T : struct, Enum
        {
            if (Cache<T>.UnderlyingType == typeof(ulong))
            {
                ref var @enum = ref Unsafe.As<ulong, T>(ref value);
                return IsDefined(@enum);
            }
            throw new ArgumentException(IsDefinedTypeMismatchMessage);
        }


        /// <summary>
        /// Returns an indication whether a constant with a specified name exists in a specified enumeration.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(string name)
            where T : struct, Enum
            => TryParseName<T>(name, false, out _);
        #endregion


        #region Parse / TryParse
        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static T Parse<T>(string value)
            where T : struct, Enum
            => TryParseInternal<T>(value, false, out var result)
            ? result
            : throw new ArgumentException(nameof(value));


        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-insensitive.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static T Parse<T>(string value, bool ignoreCase)
            where T : struct, Enum
            => TryParseInternal<T>(value, ignoreCase, out var result)
            ? result
            : throw new ArgumentException(nameof(value));


        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
        public static bool TryParse<T>(string value, out T result)
            where T : struct, Enum
            => TryParseInternal(value, false, out result);


        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-sensitive.
        /// The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="result"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
        public static bool TryParse<T>(string value, bool ignoreCase, out T result)
            where T : struct, Enum
            => TryParseInternal(value, ignoreCase, out result);


        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-sensitive.
        /// The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="result"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        private static bool TryParseInternal<T>(string value, bool ignoreCase, out T result)
            where T : struct, Enum
        {
            if (!StartsNumber(value[0]))
                return TryParseName(value, ignoreCase, out result);

            return Type.GetTypeCode(typeof(T)) switch
            {
                TypeCode.SByte => TryParseSByte(value, out result),
                TypeCode.Byte => TryParseByte(value, out result),
                TypeCode.Int16 => TryParseInt16(value, out result),
                TypeCode.UInt16 => TryParseUInt16(value, out result),
                TypeCode.Int32 => TryParseInt32(value, out result),
                TypeCode.UInt32 => TryParseUInt32(value, out result),
                TypeCode.Int64 => TryParseInt64(value, out result),
                TypeCode.UInt64 => TryParseUInt64(value, out result),
                _ => throw new InvalidOperationException(),
            };


            #region Local Functions
            static bool TryParseSByte(string value, out T result)
            {
                if (sbyte.TryParse(value, out var converted))
                {
                    ref var @enum = ref Unsafe.As<sbyte, T>(ref converted);
                    if (Cache<T>.MemberByValue.ContainsKey(@enum))
                    {
                        result = @enum;
                        return true;
                    }
                }
                result = default;
                return false;
            }


            static bool TryParseByte(string value, out T result)
            {
                if (byte.TryParse(value, out var converted))
                {
                    ref var @enum = ref Unsafe.As<byte, T>(ref converted);
                    if (Cache<T>.MemberByValue.ContainsKey(@enum))
                    {
                        result = @enum;
                        return true;
                    }
                }
                result = default;
                return false;
            }


            static bool TryParseInt16(string value, out T result)
            {
                if (short.TryParse(value, out var converted))
                {
                    ref var @enum = ref Unsafe.As<short, T>(ref converted);
                    if (Cache<T>.MemberByValue.ContainsKey(@enum))
                    {
                        result = @enum;
                        return true;
                    }
                }
                result = default;
                return false;
            }


            static bool TryParseUInt16(string value, out T result)
            {
                if (ushort.TryParse(value, out var converted))
                {
                    ref var @enum = ref Unsafe.As<ushort, T>(ref converted);
                    if (Cache<T>.MemberByValue.ContainsKey(@enum))
                    {
                        result = @enum;
                        return true;
                    }
                }
                result = default;
                return false;
            }


            static bool TryParseInt32(string value, out T result)
            {
                if (int.TryParse(value, out var converted))
                {
                    ref var @enum = ref Unsafe.As<int, T>(ref converted);
                    if (Cache<T>.MemberByValue.ContainsKey(@enum))
                    {
                        result = @enum;
                        return true;
                    }
                }
                result = default;
                return false;
            }


            static bool TryParseUInt32(string value, out T result)
            {
                if (uint.TryParse(value, out var converted))
                {
                    ref var @enum = ref Unsafe.As<uint, T>(ref converted);
                    if (Cache<T>.MemberByValue.ContainsKey(@enum))
                    {
                        result = @enum;
                        return true;
                    }
                }
                result = default;
                return false;
            }


            static bool TryParseInt64(string value, out T result)
            {
                if (long.TryParse(value, out var converted))
                {
                    ref var @enum = ref Unsafe.As<long, T>(ref converted);
                    if (Cache<T>.MemberByValue.ContainsKey(@enum))
                    {
                        result = @enum;
                        return true;
                    }
                }
                result = default;
                return false;
            }


            static bool TryParseUInt64(string value, out T result)
            {
                if (ulong.TryParse(value, out var converted))
                {
                    ref var @enum = ref Unsafe.As<ulong, T>(ref converted);
                    if (Cache<T>.MemberByValue.ContainsKey(@enum))
                    {
                        result = @enum;
                        return true;
                    }
                }
                result = default;
                return false;
            }
            #endregion
        }


        /// <summary>
        /// Checks whether specified charactor is number.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool StartsNumber(char c)
            => char.IsDigit(c) || c == '-' || c == '+';


        /// <summary>
        /// Converts the string representation of the name of one or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-sensitive.
        /// The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="result"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        private static bool TryParseName<T>(string name, bool ignoreCase, out T result)
            where T : struct, Enum
        {
            if (ignoreCase)
            {
                var left = name.AsSpan();
                foreach (var member in Cache<T>.Members)
                {
                    var right = member.Name.AsSpan();
                    if (left.Equals(right, StringComparison.OrdinalIgnoreCase))
                    {
                        result = member.Value;
                        return true;
                    }
                }
            }
            else
            {
                if (Cache<T>.MemberByName.TryGetValue(name, out var member))
                {
                    result = member.Value;
                    return true;
                }
            }
            result = default;
            return false;
        }
        #endregion


        #region Inner Classes
        /// <summary>
        /// Provides cache for enum type members.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        internal static class Cache<T>
            where T : struct, Enum
        {
            #region Properties
            /// <summary>
            /// Returns the type of the specified enumeration.
            /// </summary>
            public static Type Type { get; }


            /// <summary>
            /// Returns the underlying type of the specified enumeration.
            /// </summary>
            public static Type UnderlyingType { get; }


            /// <summary>
            /// Returns whether the <see cref="FlagsAttribute"/> is defined.
            /// </summary>
            public static bool IsFlags { get; }


            /// <summary>
            /// Retrieves an array of the values of the constants in a specified enumeration.
            /// </summary>
            public static T[] Values { get; }


            /// <summary>
            /// Retrieves an array of the names of the constants in a specified enumeration.
            /// </summary>
            public static string[] Names { get; }


            /// <summary>
            /// Retrieves an array of the member information of the constants in a specified enumeration.
            /// </summary>
            public static Member<T>[] Members { get; }


            /// <summary>
            /// Retrieves a member information of the constants in a specified enumeration by value.
            /// </summary>
            public static FrozenDictionary<T, Member<T>> MemberByValue { get; }


            /// <summary>
            /// Retrieves a member information of the constants in a specified enumeration by name.
            /// </summary>
            public static StringKeyFrozenDictionary<Member<T>> MemberByName { get; }
            #endregion


            #region Constructors
            /// <summary>
            /// Called when this type is used for the first time.
            /// </summary>
            static Cache()
            {
                Type = typeof(T);
                UnderlyingType = Enum.GetUnderlyingType(Type);
                IsFlags = Attribute.IsDefined(Type, typeof(FlagsAttribute));
                Values = Enum.GetValues(Type) as T[];
                Names = Enum.GetNames(Type).Select(string.Intern).ToArray();
                Members = Names.Select(x => new Member<T>(x)).ToArray();
                MemberByValue = Members.Distinct(new Member<T>.ValueComparer()).ToFrozenDictionary(x => x.Value);
                MemberByName = Members.ToStringKeyFrozenDictionary(x => x.Name);
            }
            #endregion
        }

        /// <summary>
        /// Provides cache for enum attributes.
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        /// <typeparam name="TAttribute">Attribute Type</typeparam>
        internal static class EnumAttributeCache<T, TAttribute>
               where T : struct, Enum
               where TAttribute : Attribute
        {
            internal static FrozenDictionary<T, TAttribute> Cache { get; } = GetValues<T>()
                .ToFrozenDictionary(
                x => x,
                x => x.ToMember().FieldInfo.GetCustomAttribute<TAttribute>());
        }
        #endregion
    }
}
