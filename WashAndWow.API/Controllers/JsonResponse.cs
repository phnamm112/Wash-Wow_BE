﻿namespace EXE2_Wash_Wow.Controllers
{
    /// <summary>
    /// Implicit wrapping of types that serialize to non-complex values.
    /// </summary>
    /// <typeparam name="T">Types such as string, Guid, int, long, etc.</typeparam>
    public class JsonResponse<T>
    {
        public JsonResponse(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
