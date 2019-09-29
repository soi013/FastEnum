﻿using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using FastEnumUtility.Benchmark.Models;
using FastEnumUtility.Internals;



namespace FastEnumUtility.Benchmark.Scenarios
{
    public class DictionaryEnumKeyBenchmark
    {
        private const Fruits LookupKey = Fruits.Pear;


        private Dictionary<Fruits, Member<Fruits>> Standard { get; set; }
        private FrozenDictionary<Fruits, Member<Fruits>> Frozen { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            var members = FastEnum.GetMembers<Fruits>();
            this.Standard = members.ToDictionary(x => x.Value);
            this.Frozen = members.ToFrozenDictionary(x => x.Value);
        }


        [Benchmark(Baseline = true)]
        public bool Dictionary()
            => this.Standard.TryGetValue(LookupKey, out _);


        [Benchmark]
        public bool FrozenDictionary()
            => this.Frozen.TryGetValue(LookupKey, out _);
    }
}
