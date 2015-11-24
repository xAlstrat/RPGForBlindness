using System;
	public struct EnemyReturn
	{
		public EnemyAction action;
		public Object[] values;

		public EnemyReturn(EnemyAction action, Object[] values){
			this.action = action;
			this.values = values;
		}
	}
