import { useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { loginUser, LoginUserForm } from "../Api/IdentityData";
import {
  loggingUserAction,
  loginedUserAction,
} from "../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import { FormButtonContainer, PrimaryButton } from "../Styles/Common/Buttons";
import {
  FieldContainer,
  FieldError,
  FieldInput,
  FieldLabel,
  Fieldset,
} from "../Styles/Common/FieldStyles";
import { Page } from "./General/Page";

export const SignInPage = () => {
  const dispatch = useDispatch();
  const returnUrl = useSelector((state: AppState) => state.identity.returnUrl);
  const navigate = useNavigate();
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
  } = useForm<LoginUserForm>({ mode: "onBlur" });
  const [loginErrors, setLoginErrors] = useState<string[] | undefined>([]);

  const submitForm = async (data: LoginUserForm) => {
    setLoginErrors([]);
    dispatch(loggingUserAction());
    const result = await loginUser(data);
    if (result.success) {
      dispatch(loginedUserAction(result));
      navigate(returnUrl === "" ? "/" : returnUrl);
    } else setLoginErrors(result.errors);
  };

  return (
    <Page title="Sign In">
      <form onSubmit={handleSubmit(submitForm)}>
        <Fieldset disabled={isSubmitting}>
          <FieldContainer title="Input your username or email">
            <FieldLabel htmlFor="login">Login</FieldLabel>
            <FieldInput
              id="login"
              {...register("login", { required: "Login is required!" })}
              type="text"
            />
            {errors.login && <FieldError>{errors.login.message}</FieldError>}
          </FieldContainer>
          <FieldContainer>
            <FieldLabel htmlFor="password">Password</FieldLabel>
            <FieldInput
              id="password"
              {...register("password", { required: "Password is required!" })}
              type="password"
            />
            {errors.password && (
              <FieldError>{errors.password.message}</FieldError>
            )}
          </FieldContainer>
          {loginErrors &&
            loginErrors.map((error) => <FieldError>{error}</FieldError>)}
          <FormButtonContainer>
            <PrimaryButton type="submit">Login</PrimaryButton>
          </FormButtonContainer>
        </Fieldset>
      </form>
    </Page>
  );
};
