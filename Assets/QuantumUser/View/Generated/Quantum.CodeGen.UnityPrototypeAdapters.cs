// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial
// declarations in another file.
// </auto-generated>
#pragma warning disable 0109
#pragma warning disable 1591


namespace Quantum.Prototypes.Unity {
  using Photon.Deterministic;
  using Quantum;
  using Quantum.Core;
  using Quantum.Collections;
  using Quantum.Inspector;
  using Quantum.Physics2D;
  using Quantum.Physics3D;
  using Byte = System.Byte;
  using SByte = System.SByte;
  using Int16 = System.Int16;
  using UInt16 = System.UInt16;
  using Int32 = System.Int32;
  using UInt32 = System.UInt32;
  using Int64 = System.Int64;
  using UInt64 = System.UInt64;
  using Boolean = System.Boolean;
  using String = System.String;
  using Object = System.Object;
  using FlagsAttribute = System.FlagsAttribute;
  using SerializableAttribute = System.SerializableAttribute;
  using MethodImplAttribute = System.Runtime.CompilerServices.MethodImplAttribute;
  using MethodImplOptions = System.Runtime.CompilerServices.MethodImplOptions;
  using FieldOffsetAttribute = System.Runtime.InteropServices.FieldOffsetAttribute;
  using StructLayoutAttribute = System.Runtime.InteropServices.StructLayoutAttribute;
  using LayoutKind = System.Runtime.InteropServices.LayoutKind;
  #if QUANTUM_UNITY //;
  using TooltipAttribute = UnityEngine.TooltipAttribute;
  using HeaderAttribute = UnityEngine.HeaderAttribute;
  using SpaceAttribute = UnityEngine.SpaceAttribute;
  using RangeAttribute = UnityEngine.RangeAttribute;
  using HideInInspectorAttribute = UnityEngine.HideInInspector;
  using PreserveAttribute = UnityEngine.Scripting.PreserveAttribute;
  using FormerlySerializedAsAttribute = UnityEngine.Serialization.FormerlySerializedAsAttribute;
  using MovedFromAttribute = UnityEngine.Scripting.APIUpdating.MovedFromAttribute;
  using CreateAssetMenu = UnityEngine.CreateAssetMenuAttribute;
  using RuntimeInitializeOnLoadMethodAttribute = UnityEngine.RuntimeInitializeOnLoadMethodAttribute;
  #endif //;
  
  [System.SerializableAttribute()]
  public unsafe partial class AnimatorComponentPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.AnimatorComponentPrototype> {
    public AssetRef<AnimatorGraph> AnimatorGraph;
    public Quantum.QuantumEntityPrototype Self;
    [HideInInspector()]
    public FP Time;
    [HideInInspector()]
    public FP NormalizedTime;
    [HideInInspector()]
    public FP LastTime;
    [HideInInspector()]
    public FP Length;
    [HideInInspector()]
    public Int32 CurrentStateId;
    [HideInInspector()]
    public QBoolean Freeze;
    [HideInInspector()]
    public FP Speed;
    [HideInInspector()]
    public Int32 FromStateId;
    [HideInInspector()]
    public FP FromStateTime;
    [HideInInspector()]
    public FP FromStateLastTime;
    [HideInInspector()]
    public FP FromStateNormalizedTime;
    [HideInInspector()]
    public FP FromLength;
    [HideInInspector()]
    public Int32 ToStateId;
    [HideInInspector()]
    public FP ToStateTime;
    [HideInInspector()]
    public FP ToStateLastTime;
    [HideInInspector()]
    public FP ToStateNormalizedTime;
    [HideInInspector()]
    public FP ToLength;
    [HideInInspector()]
    public Int32 TransitionIndex;
    [HideInInspector()]
    public FP TransitionTime;
    [HideInInspector()]
    public FP TransitionDuration;
    [HideInInspector()]
    public Int32 AnimatorBlendCount;
    public QBoolean IgnoreTransitions;
    [HideInInspector()]
    [DynamicCollectionAttribute()]
    public Quantum.Prototypes.AnimatorRuntimeVariablePrototype[] AnimatorVariables = {};
    [HideInInspector()]
    [DictionaryAttribute()]
    [DynamicCollectionAttribute()]
    public DictionaryEntry_Int32_BlendTreeWeights[] BlendTreeWeights = {};
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.AnimatorComponentPrototype prototype);
    public override Quantum.Prototypes.AnimatorComponentPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.AnimatorComponentPrototype();
      converter.Convert(this.AnimatorGraph, out result.AnimatorGraph);
      converter.Convert(this.Self, out result.Self);
      converter.Convert(this.Time, out result.Time);
      converter.Convert(this.NormalizedTime, out result.NormalizedTime);
      converter.Convert(this.LastTime, out result.LastTime);
      converter.Convert(this.Length, out result.Length);
      converter.Convert(this.CurrentStateId, out result.CurrentStateId);
      converter.Convert(this.Freeze, out result.Freeze);
      converter.Convert(this.Speed, out result.Speed);
      converter.Convert(this.FromStateId, out result.FromStateId);
      converter.Convert(this.FromStateTime, out result.FromStateTime);
      converter.Convert(this.FromStateLastTime, out result.FromStateLastTime);
      converter.Convert(this.FromStateNormalizedTime, out result.FromStateNormalizedTime);
      converter.Convert(this.FromLength, out result.FromLength);
      converter.Convert(this.ToStateId, out result.ToStateId);
      converter.Convert(this.ToStateTime, out result.ToStateTime);
      converter.Convert(this.ToStateLastTime, out result.ToStateLastTime);
      converter.Convert(this.ToStateNormalizedTime, out result.ToStateNormalizedTime);
      converter.Convert(this.ToLength, out result.ToLength);
      converter.Convert(this.TransitionIndex, out result.TransitionIndex);
      converter.Convert(this.TransitionTime, out result.TransitionTime);
      converter.Convert(this.TransitionDuration, out result.TransitionDuration);
      converter.Convert(this.AnimatorBlendCount, out result.AnimatorBlendCount);
      converter.Convert(this.IgnoreTransitions, out result.IgnoreTransitions);
      converter.Convert(this.AnimatorVariables, out result.AnimatorVariables);
      converter.Convert(this.BlendTreeWeights, out result.BlendTreeWeights);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class KCCPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.KCCPrototype> {
    public AssetRef<KCCSettings> Settings;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.KCCPrototype prototype);
    public override Quantum.Prototypes.KCCPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.KCCPrototype();
      converter.Convert(this.Settings, out result.Settings);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class KCCCollisionPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.KCCCollisionPrototype> {
    public Quantum.QEnum8<EKCCCollisionSource> Source;
    public Quantum.QuantumEntityPrototype Reference;
    public AssetRef Processor;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.KCCCollisionPrototype prototype);
    public override Quantum.Prototypes.KCCCollisionPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.KCCCollisionPrototype();
      converter.Convert(this.Source, out result.Source);
      converter.Convert(this.Reference, out result.Reference);
      converter.Convert(this.Processor, out result.Processor);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class KCCIgnorePrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.KCCIgnorePrototype> {
    public Quantum.QEnum8<EKCCIgnoreSource> Source;
    public Quantum.QuantumEntityPrototype Reference;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.KCCIgnorePrototype prototype);
    public override Quantum.Prototypes.KCCIgnorePrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.KCCIgnorePrototype();
      converter.Convert(this.Source, out result.Source);
      converter.Convert(this.Reference, out result.Reference);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class KCCModifierPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.KCCModifierPrototype> {
    public AssetRef Processor;
    public Quantum.QuantumEntityPrototype Entity;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.KCCModifierPrototype prototype);
    public override Quantum.Prototypes.KCCModifierPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.KCCModifierPrototype();
      converter.Convert(this.Processor, out result.Processor);
      converter.Convert(this.Entity, out result.Entity);
      ConvertUser(converter, ref result);
      return result;
    }
  }
}
#pragma warning restore 0109
#pragma warning restore 1591
