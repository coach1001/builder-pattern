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
            new Sieve() {Size =  "105"},
            new Sieve() {Size =  "75"},
            new Sieve() {Size =  "63"},
            new Sieve() {Size =  "53"},
            new Sieve() {Size =  "37.5"},
            new Sieve() {Size =  "26.5"},
            new Sieve() {Size =  "19"},
            new Sieve() {Size =  "13.2"},
            new Sieve() {Size =  "19"},
            new Sieve() {Size =  "13.2"},
            new Sieve() {Size =  "4.75"},
            new Sieve() {Size =  "2"},
            new Sieve() {Size =  "0.425"},
            new Sieve() {Size =  "< 0.425"},
        };
    }
}
