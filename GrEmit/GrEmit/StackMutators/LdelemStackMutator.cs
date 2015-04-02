using System;
using System.Collections.Generic;

using GrEmit.InstructionParameters;

namespace GrEmit.StackMutators
{
    internal class LdelemStackMutator : StackMutator
    {
        public override void Mutate(GroboIL il, ILInstructionParameter parameter, ref Stack<Type> stack)
        {
            var elementType = ((TypeILInstructionParameter)parameter).Type;
            CheckNotEmpty(il, stack);
            CheckCanBeAssigned(il, typeof(int), stack.Pop());
            CheckNotEmpty(il, stack);
            var peek = stack.Pop();
            if(!peek.IsArray && peek != typeof(Array))
                throw new InvalidOperationException("An array expected but was '" + peek + "'");
            CheckCanBeAssigned(il, elementType, peek.GetElementType());
            stack.Push(elementType);
        }
    }
}