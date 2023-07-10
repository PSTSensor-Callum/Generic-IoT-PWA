using Microsoft.AspNetCore.Mvc;

namespace Generic_IoT_PWA.Data.Helpers
{
    public static class ActionResultHelper
    {
        public static string DoesNotExistMessage(string className, List<Guid> ids) =>
        ids.Count > 1
            // commas between every id, except last, which is seperated with an and. each id is also surrounded in quotes
            ? $"{className}: {string.Join(", ", ids.Take(ids.Count - 1).Select(x => $"'{x}'"))} and '{ids.Last()}' do not exist."
            : $"{className}: \"{ids.First()}\" does not exist.";

        public static ActionResult DoesNotExist(ControllerBase controllerBase, string className, Guid id) =>
       controllerBase.NotFound(DoesNotExistMessage(className, new List<Guid>() { id }));

        public static string AlreadyExistsInMessage(string className, string propertyName, Guid id) =>
        AlreadyExistsInMessage(className, propertyName, new List<Guid>() { id });

        public static string AlreadyExistsInMessage(string className, string propertyName, List<Guid> ids)
        {
            string baseString = $"'{className}.{propertyName}': already contains";
            return ids.Count > 1
                ? $"{baseString} {string.Join(", ", ids.Take(ids.Count - 1).Select(x => $"'{x}'"))} and '{ids.Last()}'"
                : $"{baseString} '{ids.First()}'";
        }

        public static ActionResult AlreadyExistsIn(ControllerBase controllerBase, string className, string propertyName, Guid id) =>
            controllerBase.BadRequest(AlreadyExistsInMessage(className, propertyName, id));

        public static ActionResult AlreadyExistsIn(ControllerBase controllerBase, string className, string propertyName, List<Guid> ids) =>
        controllerBase.BadRequest(AlreadyExistsInMessage(className, propertyName, ids));

        public static string EmptyParameterMessage(string parameterName) =>
        $"Parameter \"{parameterName}\" was missing or empty";

        public static ActionResult EmptyParameter(this ControllerBase controllerBase, string parameterName) =>
        controllerBase.BadRequest(EmptyParameterMessage(parameterName));
    }
}
