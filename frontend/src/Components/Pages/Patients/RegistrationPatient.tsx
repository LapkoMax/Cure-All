import { useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import {
  registerPatient,
  RegisterPatientForm,
} from "../../../Api/IdentityData";
import {
  loggingUserAction,
  loginedUserAction,
} from "../../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import {
  FormButtonContainer,
  PrimaryButton,
  SecondaryButton,
} from "../../../Styles/Common/Buttons";
import {
  FieldContainer,
  FieldError,
  FieldInput,
  FieldLabel,
  RegistrationFieldset,
} from "../../../Styles/Common/FieldStyles";

export const RegistrationPatient = () => {
  const dispatch = useDispatch();
  const returnUrl = useSelector((state: AppState) => state.identity.returnUrl);
  const navigate = useNavigate();
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
    watch,
  } = useForm<RegisterPatientForm>({ mode: "onBlur" });
  const password = useRef({});
  password.current = watch("password", "");
  const [loginErrors, setLoginErrors] = useState<string[] | undefined>([]);

  const submitForm = async (data: RegisterPatientForm) => {
    setLoginErrors([]);
    dispatch(loggingUserAction());
    const result = await registerPatient(data);
    if (result.success) {
      dispatch(loginedUserAction(result));
      navigate(returnUrl === "" ? "/" : returnUrl);
    } else setLoginErrors(result.errors);
  };

  return (
    <form
      onSubmit={handleSubmit(submitForm)}
      className="row d-flex justify-content-around"
    >
      <RegistrationFieldset
        disabled={isSubmitting}
        className="row d-flex justify-content-around"
      >
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="firstName">Имя</FieldLabel>
          <FieldInput
            id="firstName"
            {...register("firstName", {
              required: "Имя обязательно!",
              minLength: {
                value: 2,
                message: "Имя должно состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 50,
                message: "Имя должно состоять максимум из 50 символов!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.firstName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="lastName">Фамилия</FieldLabel>
          <FieldInput
            id="lastName"
            {...register("lastName", {
              required: "Фамилия обязательна!",
              minLength: {
                value: 2,
                message: "Фамилия должна состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 50,
                message: "Фамилия должна состоять максимум из 50 символов!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.lastName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="userName">Имя пользователя</FieldLabel>
          <FieldInput
            id="userName"
            {...register("userName", {
              required: "Имя пользователя обязательно!",
              minLength: {
                value: 2,
                message:
                  "Имя пользователя должно состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 20,
                message:
                  "Имя пользователя должно состоять максимум из 20 символов!",
              },
              pattern: {
                value: /^[a-z]{2,}\d*$/i,
                message:
                  "Имя пользователя должно состоять из латинских символов, допускается наличие чисел!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.userName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="email">Эл. почта</FieldLabel>
          <FieldInput
            id="email"
            {...register("email", {
              required: "Почта обязательна!",
              pattern: {
                value:
                  /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
                message: "Неверный формат адреса почты!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.email?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="dateOfBurth">Дата рождения</FieldLabel>
          <FieldInput
            id="dateOfBurth"
            {...register("dateOfBurth", {
              required: "Дата рождения обязательна!",
            })}
            type="date"
            lang="ru-Cyrl-BY"
          />
          <FieldError>{errors.dateOfBurth?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="phoneNumber">Номер телефона</FieldLabel>
          <FieldInput
            id="phoneNumber"
            {...register("phoneNumber", {
              required: "Номер телефона обязателен!",
              pattern: {
                value:
                  /^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$/im,
                message: "Неверный формат номера!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.phoneNumber?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="zipCode">Почтовый код</FieldLabel>
          <FieldInput
            id="zipCode"
            {...register("zipCode", {
              required: "Почтовый код обязателен!",
              pattern: {
                value: /[0-9]{5}/,
                message: "Код должен состоять из 5 цифр!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.zipCode?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="country">Страна</FieldLabel>
          <FieldInput
            id="country"
            {...register("country", {
              required: "Страна обязательна!",
              minLength: {
                value: 3,
                message: "Название страны не может быть меньше 3 символов!",
              },
              maxLength: {
                value: 50,
                message: "Название страны не может быть больше 50 символов!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.country?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="city">Город</FieldLabel>
          <FieldInput
            id="city"
            {...register("city", {
              required: "Город обязателен!",
              minLength: {
                value: 3,
                message: "Название города не может быть меньше 3 символов!",
              },
              maxLength: {
                value: 50,
                message: "Название города не может быть больше 50 символов!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.city?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="password">Пароль</FieldLabel>
          <FieldInput
            id="password"
            {...register("password", {
              required: "Пароль обязателен!",
              minLength: {
                value: 8,
                message: "Пароль должен состоять минимум из 8 символов!",
              },
              validate: {
                latinCharacters: (value) =>
                  !new RegExp("^[а-яА-ЯёЁ]").test(value) ||
                  "Пароль не должен содержать кириллицу",
                number: (value) =>
                  /\d/.test(value) || "Пароль должен содержать минимум 1 цифру",
                capitalLetter: (value) =>
                  new RegExp("[A-Z]").test(value) ||
                  "Пароль должен содержать минимум 1 заглавный символ",
                specialCharacters: (value) =>
                  /[\W_]+/g.test(value) ||
                  "Пароль должен содержать минимум 1 спец. символ",
              },
            })}
            type="password"
          />
          <FieldError>{errors.password?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="confirmPassword">Подтвердите пароль</FieldLabel>
          <FieldInput
            id="confirmPassword"
            {...register("confirmPassword", {
              required: "Подтверждение пароля обязательно!",
              validate: (value) =>
                value === password.current || "Пароль подтверждён неверно",
            })}
            type="password"
          />
          <FieldError>{errors.confirmPassword?.message}</FieldError>
        </FieldContainer>
        {loginErrors &&
          loginErrors.map((error) => (
            <FieldError key={error}>{error}</FieldError>
          ))}
        <FormButtonContainer className="row d-flex justify-content-around">
          <PrimaryButton
            type="submit"
            className="col-4 d-flex justify-content-center"
          >
            Зарегистрироваться
          </PrimaryButton>
          <SecondaryButton
            onClick={() => {
              navigate(returnUrl === "" ? "/" : returnUrl);
            }}
            className="col-4 d-flex justify-content-center"
          >
            Назад
          </SecondaryButton>
        </FormButtonContainer>
      </RegistrationFieldset>
    </form>
  );
};
