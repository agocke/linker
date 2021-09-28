
# Formal Description of Data-flow Analysis

The linker does [data-flow analysis](https://en.wikipedia.org/wiki/Data-flow_analysis) of method IL to analyze reflection calls.

This document describes the formal model used by the linker.

The reflection tracking in the analysis centers around `DynamicallyAccessedMembers`. When the linker sees