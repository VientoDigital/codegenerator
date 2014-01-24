using System;

namespace iCodeGenerator.Generator
{
	public abstract class Expression
	{
		public abstract void Interpret(Context context);
		public virtual void AddExpression(Expression expression)
		{
			throw new NotImplementedException();
		}
		public virtual void RemoveExpression(Expression expression)
		{
			throw new NotImplementedException();
		}
		private object _parameter;
		
		internal object Parameter
		{
			set { _parameter = value; }
			get { return _parameter; }
		}		
	}
}
