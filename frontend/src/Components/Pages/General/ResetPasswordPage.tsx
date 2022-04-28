import { useRef } from "react";
import { useForm } from "react-hook-form";
import { useNavigate, useSearchParams } from "react-router-dom";
import { resetPassword } from "../../../Api/IdentityData";
import { PrimaryButton } from "../../../Styles/Common/Buttons";
import {
  FieldContainer,
  FieldError,
  FieldInput,
  FieldLabel,
} from "../../../Styles/Common/FieldStyles";
import { Page } from "../../General/Page";

interface ResetPasswordForm {
  newPassword: string;
  confirmPassword: string;
}

export const ResetPasswordPage = () => {
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token") || "";
  const email = searchParams.get("email") || "";
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
    watch,
  } = useForm<ResetPasswordForm>({ mode: "onBlur" });
  const navigate = useNavigate();
  const password = useRef({});
  password.current = watch("newPassword", "");

  const submitForm = async (form: ResetPasswordForm) => {
    if (email !== "") {
      const doResetPassword = async (
        email: string,
        token: string,
        newPassword: string,
      ) => {
        var result = await resetPassword(email, token, newPassword);
        if (result) navigate("/login");
      };
      doResetPassword(email, token, form.newPassword);
    }
  };

  return (
    <Page title="Восстановление пароля">
      <form onSubmit={handleSubmit(submitForm)}>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="newPassword">Пароль</FieldLabel>
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
          <FieldError>{errors.newPassword?.message}</FieldError>
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
        <PrimaryButton type="submit" disabled={isSubmitting}>
          Сменить пароль
        </PrimaryButton>
      </form>
    </Page>
  );
};
