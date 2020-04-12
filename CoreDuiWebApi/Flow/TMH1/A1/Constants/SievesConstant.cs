using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow.TMH1.A1.FlowConstants
{
    public class SievesConstant
    {
        public static readonly ICollection<Sieve> A1Sieves = new List<Sieve>
        {
            new Sieve() {Id__ = null, Size =  "105"},
            new Sieve() {Id__ = null, Size =  "75"},
            new Sieve() {Id__ = null, Size =  "63"},
            new Sieve() {Id__ = null, Size =  "53"},
            new Sieve() {Id__ = null, Size =  "37.5"},
            new Sieve() {Id__ = null, Size =  "26.5"},
            new Sieve() {Id__ = null, Size =  "19"},
            new Sieve() {Id__ = null, Size =  "13.2"},
            new Sieve() {Id__ = null, Size =  "19"},
            new Sieve() {Id__ = null, Size =  "13.2"},
            new Sieve() {Id__ = null, Size =  "4.75"},
            new Sieve() {Id__ = null, Size =  "2"},
            new Sieve() {Id__ = null, Size =  "0.425"},
            new Sieve() {Id__ = null, Size =  "< 0.425"},
        };
    }
}
