using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Codelux.ServiceStack.OrmLite
{
    public static class ReflectionHelper
    {
        public static MemberInfo GetMemberInfo(LambdaExpression lambda)
        {
            Expression expr = lambda;
            while (true)
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;

                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;

                    case ExpressionType.MemberAccess:
                        MemberExpression memberExpression = (MemberExpression)expr;
                        MemberInfo baseMember = memberExpression.Member;

                        Type paramType = lambda.Parameters[0].Type;
                        MemberInfo memberInfo = paramType.GetMember(baseMember.Name)[0];
                        return memberInfo;

                    default:
                        return null;
                }
            }
        }
    }
}
