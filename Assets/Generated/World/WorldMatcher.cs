//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class WorldMatcher {

    public static Entitas.IAllOfMatcher<WorldEntity> AllOf(params int[] indices) {
        return Entitas.Matcher<WorldEntity>.AllOf(indices);
    }

    public static Entitas.IAllOfMatcher<WorldEntity> AllOf(params Entitas.IMatcher<WorldEntity>[] matchers) {
          return Entitas.Matcher<WorldEntity>.AllOf(matchers);
    }

    public static Entitas.IAnyOfMatcher<WorldEntity> AnyOf(params int[] indices) {
          return Entitas.Matcher<WorldEntity>.AnyOf(indices);
    }

    public static Entitas.IAnyOfMatcher<WorldEntity> AnyOf(params Entitas.IMatcher<WorldEntity>[] matchers) {
          return Entitas.Matcher<WorldEntity>.AnyOf(matchers);
    }
}
