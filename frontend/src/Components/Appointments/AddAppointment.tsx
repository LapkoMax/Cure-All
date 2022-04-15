import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import {
  createAppointment,
  CreateAppointmentForm,
} from "../../Api/AppointmentsData";
import { DoctorData } from "../../Api/DoctorsData";
import { getPatient } from "../../Api/PatientsData";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
import {
  gettingPatientAction,
  gotPatientAction,
} from "../../Store/ActionCreators/PatientActionCreators";
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
  FieldTextArea,
} from "../../Styles/Common/FieldStyles";

type Props = {
  doctor?: DoctorData | null;
};

export const AddAppointment = ({ doctor }: Props) => {
  const navigate = useNavigate();
  const location = useLocation();
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
  } = useForm<CreateAppointmentForm>({ mode: "onBlur" });
  const dispatch = useDispatch();
  const patient = useSelector((state: AppState) => state.patients.patient);
  const user = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [createErrors, setCreateErrors] = useState<string[] | undefined>([]);

  useEffect(() => {
    const doGetPatient = async (userId?: string) => {
      dispatch(gettingPatientAction());
      var result = await getPatient(userToken, userId);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      else if (result.data !== null) dispatch(gotPatientAction(result.data));
    };
    doGetPatient(user?.id);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user]);

  const submitForm = async (data: CreateAppointmentForm) => {
    setCreateErrors([]);
    const result = await createAppointment(data, userToken);
    if (result.length === 1 && result[0].includes("Its OK: ")) {
      navigate("appointment/" + result[0].replace("Its OK: ", ""));
    } else if (result[0] === "Unauthorized")
      dispatch(signOutUserAction(location.pathname));
    else setCreateErrors(result);
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
        <FieldInput
          {...register("patientCardId")}
          value={patient?.patientCardId}
          hidden={true}
        />
        <FieldInput
          {...register("doctorId")}
          value={doctor?.id}
          hidden={true}
        />
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="description">Опишите вашу проблему:</FieldLabel>
          <FieldTextArea
            id="description"
            {...register("description", {
              required: "Описание обязательно!",
              minLength: {
                value: 2,
                message: "Описание должно состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 300,
                message: "Описание должно состоять максимум из 300 символов!",
              },
            })}
          />
          <FieldError>{errors.description?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="startDate">
            Укажите предпочтительные дату и время приёма:
          </FieldLabel>
          <FieldInput
            id="startDate"
            {...register("startDate", { required: "Дата приёма обязательна!" })}
            type="datetime-local"
          />
          <FieldError>{errors.startDate?.message}</FieldError>
        </FieldContainer>
        {createErrors &&
          createErrors.map((error) => (
            <FieldError key={error}>{error}</FieldError>
          ))}
        <FormButtonContainer className="row d-flex justify-content-around">
          <PrimaryButton
            type="submit"
            className="col-4 d-flex justify-content-center"
          >
            Отправить запрос
          </PrimaryButton>
          <SecondaryButton
            onClick={() => {
              navigate("/profile/" + doctor?.userId);
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
