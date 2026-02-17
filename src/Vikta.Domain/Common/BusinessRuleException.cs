namespace Vikta.Domain.Common;

public sealed class BusinessRuleException(string message) : Exception(message);
