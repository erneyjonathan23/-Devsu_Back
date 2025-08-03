//using FluentValidation;

//namespace OP.Prueba.Application.Features.CreateCourseCommand.Commands.CreateCourseCommand
//{
//    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
//    {
//        public CreateCourseCommandValidator()
//        {
//            RuleFor(p => p.Name)
//               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

//            RuleFor(p => p.TotalCredits)
//               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
//        }
//    }
//}