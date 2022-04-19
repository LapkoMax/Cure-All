/** @jsxImportSource @emotion/react */
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useLocation, useNavigate, useParams } from "react-router-dom";
import {
  deleteAppointment,
  getAppointment,
  ifUserCanEditAppointment,
} from "../../../Api/AppointmentsData";
import {
  gettingAppointmentAction,
  gotAppointmentAction,
} from "../../../Store/ActionCreators/AppointmentActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import {
  appointmentAdditionalInf,
  appointmentContainer,
  appointmentTitle,
} from "../../../Styles/Appointments/AppointmentPageStyles";
import {
  DangerButton,
  FormButtonContainer,
  PrimaryButton,
} from "../../../Styles/Common/Buttons";
import { SubmissionSuccess } from "../../../Styles/Common/FieldStyles";
import { Page } from "../../General/Page";

export const AppointmentPage = () => {
  const { appointmentId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();
  const appointment = useSelector(
    (state: AppState) => state.appointments.appointment,
  );
  const appointmentLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );
  const user = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [userCanEdit, setUserCanEdit] = useState(false);

  useEffect(() => {
    const doGetAppointment = async (appointmentId?: string) => {
      dispatch(gettingAppointmentAction());
      let result = await getAppointment(appointmentId, userToken);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotAppointmentAction(result.data));
      let canChange = await ifUserCanEditAppointment(
        appointmentId,
        user?.id,
        userToken,
      );
      setUserCanEdit(canChange);
    };
    doGetAppointment(appointmentId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const editClickHandler = () => {
    navigate("edit");
  };

  const deleteClickHandler = async () => {
    let result = await deleteAppointment(appointment?.id, userToken);

    if (result) navigate("/appointments/" + user?.id);
  };

  return (
    <Page>
      {appointmentLoading ? (
        <div>Загрузка...</div>
      ) : (
        <div css={appointmentContainer} className="row">
          <div css={appointmentTitle} className="row">
            {appointment?.completed && (
              <SubmissionSuccess className="col-4 row">
                Завершено
              </SubmissionSuccess>
            )}
            <div className="col-12 row">{appointment?.description}</div>
          </div>
          <Link
            to={`/patientCard/${appointment?.patientCardId}`}
            css={appointmentAdditionalInf}
            className="col-12 row"
          >
            Пациент: {appointment?.patientFirstName}{" "}
            {appointment?.patientLastName}
          </Link>
          <Link
            to={`/profile/${appointment?.doctorUserId}`}
            css={appointmentAdditionalInf}
            className="col-12 row"
          >
            Доктор: {appointment?.doctorFirstName} {appointment?.doctorLastName}
          </Link>
          <div css={appointmentAdditionalInf} className="col-12 row">
            Заболевание:{" "}
            {appointment?.illnessName === null
              ? "Не указано"
              : appointment?.illnessName}
          </div>
          <div css={appointmentAdditionalInf} className="col-12 row">
            Дата: {appointment?.startDate.toString().substring(0, 10)}
          </div>
          <div css={appointmentAdditionalInf} className="col-12 row">
            Время: {appointment?.startTime}
          </div>
          {appointment?.endDate !== null && (
            <div css={appointmentAdditionalInf} className="col-12 row">
              Дата завершения:{" "}
              {appointment?.endDate.toString().substring(0, 10)}{" "}
              {appointment?.endDate.toString().substring(12, 16)}
            </div>
          )}
        </div>
      )}
      {userCanEdit && (
        <div className="d-flex justify-content-center">
          <FormButtonContainer className="col-12 d-flex justify-content-around">
            <PrimaryButton onClick={editClickHandler}>
              Редактировать инф. о посещении
            </PrimaryButton>
            <DangerButton onClick={deleteClickHandler}>
              Удалить посещение
            </DangerButton>
          </FormButtonContainer>
        </div>
      )}
    </Page>
  );
};
