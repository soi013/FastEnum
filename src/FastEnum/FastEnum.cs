﻿using System;
using System.Collections.Generic;
using System.Linq;
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


        #region GetNames / GetName
        /// <summary>
        /// Retrieves an array of the names of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<string> GetNames<T>()
            where T : struct, Enum
            => Cache<T>.Names;


        /// <summary>
        /// Retrieves the name of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static string GetName<T>(T value)
            where T : struct, Enum
            => GetMember(value).Name;
        #endregion


        #region GetMembers / GetMember
        /// <summary>
        /// Retrieves an array of the member information of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static IReadOnlyList<Member<T>> GetMembers<T>()
            where T : struct, Enum
            => Cache<T>.Members;


        /// <summary>
        /// Retrieves the member information of the constants in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static Member<T> GetMember<T>(T value)
            where T : struct, Enum
            => Cache<T>.MemberByValue.TryGetValue(value, out var member)
            ? member
            : throw new ArgumentException(nameof(value));
        #endregion


        #region GetMinValue / GetMaxValue
        /// <summary>
        /// Returns the minimum value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static T? GetMinValue<T>()
            where T : struct, Enum
            => Cache<T>.IsEmpty ? (T?)null : Cache<T>.MinValue;


        /// <summary>
        /// Returns the maximum value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static T? GetMaxValue<T>()
            where T : struct, Enum
            => Cache<T>.IsEmpty ? (T?)null : Cache<T>.MaxValue;
        #endregion


        #region IsEmpty
        /// <summary>
        /// Returns whether no fields in a specified enumeration.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsEmpty<T>()
            where T : struct, Enum
            => Cache<T>.IsEmpty;
        #endregion


        #region IsContinuous
        /// <summary>
        /// Returns whether the values of the constants in a specified enumeration are continuous.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsContinuous<T>()
            where T : struct, Enum
            => Cache<T>.IsContinuous;
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


        #region IsDefined
        /// <summary>
        /// Returns an indication whether a constant with a specified value exists in a specified enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        public static bool IsDefined<T>(T value)
            where T : struct, Enum
            => Cache<T>.IsDefined(value);


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

            return Cache<T>.TypeCode switch
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
            #region Fields
            public static readonly Type Type;
            public static readonly Type UnderlyingType;
            public static readonly TypeCode TypeCode;
            public static readonly T[] Values;
            public static readonly string[] Names;
            public static readonly Member<T>[] Members;
            public static T MinValue;  // no readonly for performance
            public static T MaxValue;  // no readonly for performance
            public static readonly bool IsEmpty;
            public static readonly bool IsContinuous;
            public static readonly bool IsFlags;
            public static readonly FrozenDictionary<T, Member<T>> MemberByValue;
            public static readonly FrozenStringKeyDictionary<Member<T>> MemberByName;
            #endregion


            #region Constructors
            static Cache()
            {
                Type = typeof(T);
                UnderlyingType = Enum.GetUnderlyingType(Type);
                TypeCode = Type.GetTypeCode(Type);
                Values = Enum.GetValues(Type) as T[];
                Names = Enum.GetNames(Type).Select(string.Intern).ToArray();
                Members = Names.Select(x => new Member<T>(x)).ToArray();
                MinValue = Values.DefaultIfEmpty().Min();
                MaxValue = Values.DefaultIfEmpty().Max();
                IsEmpty = Values.Length == 0;
                IsFlags = Attribute.IsDefined(Type, typeof(FlagsAttribute));
                MemberByValue = Members.Distinct(new Member<T>.ValueComparer()).ToFrozenDictionary(x => x.Value);
                MemberByName = Members.ToFrozenStringKeyDictionary(x => x.Name);
                IsContinuous = IsContinuousInternal();
            }
            #endregion


            #region Utility
            private static bool IsContinuousInternal()
            {
                if (IsEmpty)
                    return false;

                var count = MemberByValue.Count;  // distincted count
                switch (TypeCode)
                {
                    case TypeCode.SByte:
                        {
                            ref var min = ref Unsafe.As<T, sbyte>(ref MinValue);
                            ref var max = ref Unsafe.As<T, sbyte>(ref MaxValue);
                            return (max - min) == (count - 1);
                        }

                    case TypeCode.Byte:
                        {
                            ref var min = ref Unsafe.As<T, byte>(ref MinValue);
                            ref var max = ref Unsafe.As<T, byte>(ref MaxValue);
                            return (max - min) == (count - 1);
                        }

                    case TypeCode.Int16:
                        {
                            ref var min = ref Unsafe.As<T, short>(ref MinValue);
                            ref var max = ref Unsafe.As<T, short>(ref MaxValue);
                            return (max - min) == (count - 1);
                        }

                    case TypeCode.UInt16:
                        {
                            ref var min = ref Unsafe.As<T, ushort>(ref MinValue);
                            ref var max = ref Unsafe.As<T, ushort>(ref MaxValue);
                            return (max - min) == (count - 1);
                        }

                    case TypeCode.Int32:
                        {
                            ref var min = ref Unsafe.As<T, int>(ref MinValue);
                            ref var max = ref Unsafe.As<T, int>(ref MaxValue);
                            return (max - min) == (count - 1);
                        }

                    case TypeCode.UInt32:
                        {
                            ref var min = ref Unsafe.As<T, uint>(ref MinValue);
                            ref var max = ref Unsafe.As<T, uint>(ref MaxValue);
                            return (max - min) == (count - 1);
                        }

                    case TypeCode.Int64:
                        {
                            ref var min = ref Unsafe.As<T, long>(ref MinValue);
                            ref var max = ref Unsafe.As<T, long>(ref MaxValue);
                            return (max - min) == (count - 1);
                        }

                    case TypeCode.UInt64:
                        {
                            ref var min = ref Unsafe.As<T, ulong>(ref MinValue);
                            ref var max = ref Unsafe.As<T, ulong>(ref MaxValue);
                            return (max - min) == (ulong)(count - 1);
                        }

                    default:
                        throw new InvalidOperationException();
                }
            }


            public static bool IsDefined(T value)
            {
                if (IsContinuous)
                {
                    switch (TypeCode)
                    {
                        case TypeCode.SByte:
                            {
                                ref var val = ref Unsafe.As<T, sbyte>(ref value);
                                ref var min = ref Unsafe.As<T, sbyte>(ref MinValue);
                                ref var max = ref Unsafe.As<T, sbyte>(ref MaxValue);
                                return (min <= val) && (val <= max);
                            }

                        case TypeCode.Byte:
                            {
                                ref var val = ref Unsafe.As<T, byte>(ref value);
                                ref var min = ref Unsafe.As<T, byte>(ref MinValue);
                                ref var max = ref Unsafe.As<T, byte>(ref MaxValue);
                                return (min <= val) && (val <= max);
                            }

                        case TypeCode.Int16:
                            {
                                ref var val = ref Unsafe.As<T, short>(ref value);
                                ref var min = ref Unsafe.As<T, short>(ref MinValue);
                                ref var max = ref Unsafe.As<T, short>(ref MaxValue);
                                return (min <= val) && (val <= max);
                            }

                        case TypeCode.UInt16:
                            {
                                ref var val = ref Unsafe.As<T, ushort>(ref value);
                                ref var min = ref Unsafe.As<T, ushort>(ref MinValue);
                                ref var max = ref Unsafe.As<T, ushort>(ref MaxValue);
                                return (min <= val) && (val <= max);
                            }

                        case TypeCode.Int32:
                            {
                                ref var val = ref Unsafe.As<T, int>(ref value);
                                ref var min = ref Unsafe.As<T, int>(ref MinValue);
                                ref var max = ref Unsafe.As<T, int>(ref MaxValue);
                                return (min <= val) && (val <= max);
                            }

                        case TypeCode.UInt32:
                            {
                                ref var val = ref Unsafe.As<T, uint>(ref value);
                                ref var min = ref Unsafe.As<T, uint>(ref MinValue);
                                ref var max = ref Unsafe.As<T, uint>(ref MaxValue);
                                return (min <= val) && (val <= max);
                            }

                        case TypeCode.Int64:
                            {
                                ref var val = ref Unsafe.As<T, long>(ref value);
                                ref var min = ref Unsafe.As<T, long>(ref MinValue);
                                ref var max = ref Unsafe.As<T, long>(ref MaxValue);
                                return (min <= val) && (val <= max);
                            }

                        case TypeCode.UInt64:
                            {
                                ref var val = ref Unsafe.As<T, ulong>(ref value);
                                ref var min = ref Unsafe.As<T, ulong>(ref MinValue);
                                ref var max = ref Unsafe.As<T, ulong>(ref MaxValue);
                                return (min <= val) && (val <= max);
                            }

                        default:
                            throw new InvalidOperationException();
                    }
                }
                return MemberByValue.ContainsKey(value);
            }
            #endregion
        }
        #endregion
    }
}
