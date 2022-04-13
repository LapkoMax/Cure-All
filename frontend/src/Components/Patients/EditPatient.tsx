import moment from "moment";
import { Fragment, useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import {
  editPatient,
  EditPatientForm,
  PatientData,
} from "../../Api/PatientsData";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../Store/Reducers/RootReducer";
import {
  FormButtonContainer,
  PrimaryButton,
  SecondaryButton,
} from "../../Styles/Common/Buttons";
import {
  EditFieldset,
  FieldContainer,
  FieldError,
  FieldInput,
  FieldLabel,
} from "../../Styles/Common/FieldStyles";

type Props = {
  patient?: PatientData | null;
};

export const EditPatien = ({ patient }: Props) => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation();
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
    watch,
    setValue,
  } = useForm<EditPatientForm>({ mode: "onBlur" });
  const newPassword = useRef({});
  newPassword.current = watch("newPassword", "");
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [editErrors, setEditErrors] = useState<string[] | undefined>([]);
  const [changePassword, setChangePassword] = useState(false);

  useEffect(() => {
    if (patient != null) {
      setValue("firstName", patient.firstName);
      setValue("lastName", patient.lastName);
      setValue("userName", patient.userName);
      setValue("phoneNumber", patient.phoneNumber);
      setValue("email", patient.email);
      setValue(
        "dateOfBurth",
        moment(patient?.dateOfBurth).format("yyyy-MM-DD"),
      );
      setValue("zipCode", patient.zipCode);
      setValue("country", patient.country);
      setValue("city", patient.city);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const submitForm = async (data: EditPatientForm) => {
    setEditErrors([]);
    const result = await editPatient(data, userToken);
    if (result.length === 0) {
      navigate("/profile/" + patient?.userId);
    } else if (result[0] === "Unauthorized") {
      dispatch(signOutUserAction(location.pathname));
    } else setEditErrors(result);
  };

  return (
    <form
      onSubmit={handleSubmit(submitForm)}
      className="row d-flex justify-content-around"
    >
      <EditFieldset
        disabled={isSubmitting}
        className="row d-flex justify-content-around"
      >
        <FieldInput {...register("id")} value={patient?.id} hidden={true} />
        <FieldInput
          {...register("userId")}
          value={patient?.userId}
          hidden={true}
        />
        <FieldInput
          {...register("oldUserName")}
          value={patient?.userName}
          hidden={true}
        />
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
              required: "Имя пользоветеля обязательно!",
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
        <PrimaryButton
          onClick={() => {
            setChangePassword(!changePassword);
          }}
          className="col-4"
          hidden={changePassword}
        >
          Сменить пароль
        </PrimaryButton>
        {changePassword && (
          <Fragment>
            <FieldContainer className="row col-7 d-flex justify-content-center">
              <FieldLabel htmlFor="oldPassword">Предыдущий пароль</FieldLabel>
              <FieldInput
                id="oldPassword"
                {...register("oldPassword")}
                type="password"
              />
              <FieldError>{errors.oldPassword?.message}</FieldError>
            </FieldContainer>
            <FieldContainer className="row col-7 d-flex justify-content-center">
              <FieldLabel htmlFor="newPassword">Новый пароль</FieldLabel>
              <FieldInput
                id="newPassword"
                {...register("newPassword", {
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
                      /\d/.test(value) ||
                      "Пароль должен содержать минимум 1 цифру",
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
              <FieldError>{errors.newPassword?.message}</FieldError>
            </FieldContainer>
            <FieldContainer className="row col-7 d-flex justify-content-center">
              <FieldLabel htmlFor="confirmPassword">
                Подтвердите пароль
              </FieldLabel>
              <FieldInput
                id="confirmPassword"
                {...register("confirmPassword", {
                  required: "Подтверждение пароля обязательно!",
                  validate: (value) =>
                    value === newPassword.current ||
                    "Пароль подтверждён неверно",
                })}
                type="password"
              />
              <FieldError>{errors.confirmPassword?.message}</FieldError>
            </FieldContainer>
          </Fragment>
        )}
        {editErrors &&
          editErrors.map((error) => (
            <FieldError key={error}>{error}</FieldError>
          ))}
        <FormButtonContainer className="row d-flex justify-content-around">
          <PrimaryButton
            type="submit"
            className="col-4 d-flex justify-content-center"
          >
            Сохранить данные
          </PrimaryButton>
          <SecondaryButton
            onClick={() => {
              navigate("/profile/" + patient?.userId);
            }}
            className="col-4 d-flex justify-content-center"
          >
            Назад
          </SecondaryButton>
        </FormButtonContainer>
      </EditFieldset>
    </form>
  );
};
