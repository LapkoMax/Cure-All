import { useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { registerPatient, RegisterPatientForm } from "../Api/IdentityData";
import {
  loggingUserAction,
  loginedUserAction,
} from "../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import {
  FormButtonContainer,
  PrimaryButton,
  SecondaryButton,
} from "../Styles/Common/Buttons";
import {
  FieldContainer,
  FieldError,
  FieldInput,
  FieldLabel,
  RegistrationFieldset,
} from "../Styles/Common/FieldStyles";

export const RegistrationPatient = () => {
  const dispatch = useDispatch();
  const returnUrl = useSelector((state: AppState) => state.identity.returnUrl);
  const navigate = useNavigate();
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
  } = useForm<RegisterPatientForm>({ mode: "onBlur" });
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
          <FieldLabel htmlFor="firstName">First Name</FieldLabel>
          <FieldInput
            id="firstName"
            {...register("firstName", {
              required: "First name is required!",
              minLength: {
                value: 2,
                message: "First name must be at least 2 characters in length!",
              },
              maxLength: {
                value: 50,
                message: "First name must be 50 characters max!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.firstName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="lastName">Last Name</FieldLabel>
          <FieldInput
            id="lastName"
            {...register("lastName", {
              required: "Last name is required!",
              minLength: {
                value: 2,
                message: "Last name must be at least 2 characters in length!",
              },
              maxLength: {
                value: 50,
                message: "Last name must be 50 characters max!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.lastName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="userName">Username</FieldLabel>
          <FieldInput
            id="userName"
            {...register("userName", {
              required: "Username is required!",
              minLength: {
                value: 2,
                message: "Username must be at least 2 characters in length!",
              },
              maxLength: {
                value: 20,
                message: "Username name must be 20 characters max!",
              },
              pattern: {
                value: /^[a-zA-Z0-9]+$/,
                message: "Invalid username!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.userName?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="email">Email</FieldLabel>
          <FieldInput
            id="email"
            {...register("email", {
              required: "Email is required!",
              pattern: {
                value:
                  /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
                message: "Invalid email format!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.email?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="dateOfBurth">Date of burth</FieldLabel>
          <FieldInput
            id="dateOfBurth"
            {...register("dateOfBurth", {
              required: "Date of burth is required!",
            })}
            type="date"
          />
          <FieldError>{errors.dateOfBurth?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="phoneNumber">Phone number</FieldLabel>
          <FieldInput
            id="phoneNumber"
            {...register("phoneNumber", {
              required: "Phone number is required!",
              pattern: {
                value:
                  /^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$/im,
                message: "Invalid phone number format!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.phoneNumber?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="zipCode">Zip Code</FieldLabel>
          <FieldInput
            id="zipCode"
            {...register("zipCode", {
              required: "Zip code is required!",
              pattern: {
                value: /[0-9]{5}/,
                message: "Zip code must contain of 5 digits!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.zipCode?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="country">Country</FieldLabel>
          <FieldInput
            id="country"
            {...register("country", {
              required: "Country is required!",
              minLength: {
                value: 3,
                message:
                  "Country name must be at least 3 characters in length!",
              },
              maxLength: {
                value: 50,
                message: "Country name must be 50 characters max!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.country?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="city">City</FieldLabel>
          <FieldInput
            id="city"
            {...register("city", {
              required: "city is required!",
              minLength: {
                value: 3,
                message: "City name must be at least 3 characters in length!",
              },
              maxLength: {
                value: 50,
                message: "City name must be 50 characters max!",
              },
            })}
            type="text"
          />
          <FieldError>{errors.city?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="password">Password</FieldLabel>
          <FieldInput
            id="password"
            {...register("password", { required: "Password is required!" })}
            type="password"
          />
          <FieldError>{errors.password?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-6 d-flex justify-content-center">
          <FieldLabel htmlFor="confirmPassword">Confirm password</FieldLabel>
          <FieldInput
            id="confirmPassword"
            {...register("confirmPassword", {
              required: "Confirm password is required!",
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
            Register
          </PrimaryButton>
          <SecondaryButton
            onClick={() => {
              navigate(returnUrl === "" ? "/" : returnUrl);
            }}
            className="col-4 d-flex justify-content-center"
          >
            Cancel
          </SecondaryButton>
        </FormButtonContainer>
      </RegistrationFieldset>
    </form>
  );
};
