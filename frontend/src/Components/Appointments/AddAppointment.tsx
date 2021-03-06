/** @jsxImportSource @emotion/react */
import { Fragment, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import {
  createAppointment,
  CreateAppointmentForm,
} from "../../Api/AppointmentsData";
import {
  AvailableAppointmentTimeData,
  DoctorData,
  getDoctorAvailableTime,
} from "../../Api/DoctorsData";
import { getPatient } from "../../Api/PatientsData";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
import {
  gettingPatientAction,
  gotPatientAction,
} from "../../Store/ActionCreators/PatientActionCreators";
import { AppState } from "../../Store/Reducers/RootReducer";
import {
  availableTimeNotSelected,
  availableTimeSelected,
} from "../../Styles/Appointments/AddAppointmentStyles";
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
  const [availableTime, setAvailableTime] = useState<
    AvailableAppointmentTimeData[]
  >([]);
  const [date, setDate] = useState("");
  const [time, setTime] = useState("");

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
    data.startTime = time;
    data.patientCardId =
      patient !== null && patient !== undefined ? patient.patientCardId : "";
    const result = await createAppointment(data, userToken);
    if (result.length === 1 && result[0].includes("Its OK: ")) {
      navigate("/appointment/" + result[0].replace("Its OK: ", ""));
    } else if (result[0] === "Unauthorized")
      dispatch(signOutUserAction(location.pathname));
    else setCreateErrors(result);
  };

  const handleChangeStartDate = async (e: any) => {
    if (doctor && e.target.value !== "") {
      setDate(e.target.value);
      var result = await getDoctorAvailableTime(
        doctor?.id,
        e.target.value,
        userToken,
      );
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      else if (result.responseStatus === 200) setAvailableTime(result.data);
    }
  };

  const onTimeSelect = (time: string) => {
    setTime(time);
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
          <FieldLabel htmlFor="description">?????????????? ???????? ????????????????:</FieldLabel>
          <FieldTextArea
            id="description"
            {...register("description", {
              required: "???????????????? ??????????????????????!",
              minLength: {
                value: 2,
                message: "???????????????? ???????????? ???????????????? ?????????????? ???? 2 ????????????????!",
              },
              maxLength: {
                value: 300,
                message: "???????????????? ???????????? ???????????????? ???????????????? ???? 300 ????????????????!",
              },
            })}
          />
          <FieldError>{errors.description?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="startDate">
            ?????????????? ???????????????????????????????? ????????:
          </FieldLabel>
          <FieldInput
            id="startDate"
            {...register("startDate", { required: "???????? ???????????? ??????????????????????!" })}
            type="date"
            onChange={handleChangeStartDate}
          />
          <FieldError>{errors.startDate?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          {date !== "" && (
            <Fragment>
              <FieldLabel>???????????????? ?????????????????????????????? ??????????:</FieldLabel>
              {availableTime.length > 0 ? (
                <div className="col-12 row d-flex justify-content-around">
                  {availableTime.map((availableTime) => (
                    <div
                      css={
                        time === availableTime.time
                          ? availableTimeSelected
                          : availableTimeNotSelected
                      }
                      className="col-2 row pb-1 pt-1 mb-1 mx-1"
                      onClick={() => {
                        onTimeSelect(availableTime.time);
                      }}
                    >
                      {availableTime.time}
                    </div>
                  ))}
                </div>
              ) : (
                <div>?????? ???????????????????? ?????????????? ???? ???????? ???????? :(</div>
              )}
            </Fragment>
          )}
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
            ?????????????????? ????????????
          </PrimaryButton>
          <SecondaryButton
            onClick={() => {
              navigate("/profile/" + doctor?.userId);
            }}
            className="col-4 d-flex justify-content-center"
          >
            ??????????
          </SecondaryButton>
        </FormButtonContainer>
      </EditFieldset>
    </form>
  );
};
