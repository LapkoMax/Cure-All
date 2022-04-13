import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import {
  AppointmentData,
  editAppointment,
  EditAppointmentForm,
} from "../../Api/AppointmentsData";
import { getIllneses, IllnessData } from "../../Api/IllnesesData";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../Store/Reducers/RootReducer";
import {
  FormButtonContainer,
  PrimaryButton,
  SecondaryButton,
} from "../../Styles/Common/Buttons";
import {
  EditFieldset,
  FieldCheckBox,
  FieldContainer,
  FieldError,
  FieldLabel,
  FieldOption,
  FieldSelect,
  FieldTextArea,
} from "../../Styles/Common/FieldStyles";

type Props = {
  appointment?: AppointmentData | null;
};

export const EditAppointment = ({ appointment }: Props) => {
  const navigate = useNavigate();
  const location = useLocation();
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
    setValue,
  } = useForm<EditAppointmentForm>({ mode: "onBlur" });
  const dispatch = useDispatch();
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [editErrors, setEditErrors] = useState<string[] | undefined>([]);
  const [illneses, setIllneses] = useState<IllnessData[]>([]);

  useEffect(() => {
    const doGetIllneses = async () => {
      let results = await getIllneses();
      setIllneses(results);
    };
    doGetIllneses();

    if (appointment != null) {
      setValue("description", appointment.description);
      setValue("endDate", appointment.endDate);
      setValue("completed", appointment.completed);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (appointment == null) setValue("illnessId", null);
    else
      setValue(
        "illnessId",
        illneses.find((ill) => ill.name === appointment.illnessName)?.id ?? "",
      );
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [illneses]);

  const submitForm = async (data: EditAppointmentForm) => {
    setEditErrors([]);
    const result = await editAppointment(data, appointment?.id, userToken);
    if (result.length === 0) {
      navigate("/appointment/" + appointment?.id);
    } else if (result[0] === "Unauthorized")
      dispatch(signOutUserAction(location.pathname));
    else setEditErrors(result);
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
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="description">Описание</FieldLabel>
          <FieldTextArea
            id="description"
            {...register("description", {
              required: "Описание обязательно!",
              minLength: {
                value: 2,
                message: "Описание должно состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 150,
                message: "Описание должно состоять максимум из 150 символов!",
              },
            })}
          />
          <FieldError>{errors.description?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="illnessId">Заболевание</FieldLabel>
          <FieldSelect id="illnessId" {...register("illnessId")}>
            <FieldOption key={0} value="" disabled defaultValue="">
              Можете выбрать заболевание
            </FieldOption>
            {illneses.map((ill) => (
              <FieldOption key={ill.id} value={ill.id} title={ill.description}>
                {ill.name}
              </FieldOption>
            ))}
          </FieldSelect>
          <div className="row">
            <FieldLabel className="col-8 row">
              Нет подходящего заболевания?
            </FieldLabel>
            <PrimaryButton
              className="col-4 row"
              onClick={() => {
                navigate("/addIllness?returnUrl=" + location.pathname);
              }}
            >
              Добавьте новое
            </PrimaryButton>
          </div>
          <FieldError>{errors.illnessId?.message}</FieldError>
        </FieldContainer>
        <FieldCheckBox className="form-check col-12">
          <input
            className="form-check-input mt-3"
            type="checkbox"
            value=""
            id="completed"
            {...register("completed")}
          />
          <FieldLabel
            className="form-check-label col-lg-10 col-md-11 col-sm-11 col-form-label text-left mt-1"
            htmlFor="completed"
          >
            Завершить посещение?
          </FieldLabel>
        </FieldCheckBox>
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
              navigate("/appointment/" + appointment?.id);
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
