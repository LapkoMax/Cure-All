/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { confirmAlert } from "react-confirm-alert";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { getAppointment } from "../../../Api/AppointmentsData";
import { getDoctor } from "../../../Api/DoctorsData";
import {
  confirmNotification,
  deleteNotification,
  getNotification,
  rejectNotification,
} from "../../../Api/NotificationsData";
import {
  gettingAppointmentAction,
  gotAppointmentAction,
} from "../../../Store/ActionCreators/AppointmentActionCreators";
import {
  gettingDoctorAction,
  gotDoctorAction,
} from "../../../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import {
  gettingNotificationAction,
  gotNotificationAction,
} from "../../../Store/ActionCreators/NotificationActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import {
  DangerButton,
  FormButtonContainer,
  PrimaryButton,
  SecondaryButton,
} from "../../../Styles/Common/Buttons";
import {
  confirmAlertContainer,
  confirmAlertMessage,
  confirmAlertTitle,
} from "../../../Styles/Common/OtherComponents";
import {
  notificationAppointment,
  notificationContainer,
  notificationDesctiption,
  notificationDoctor,
} from "../../../Styles/Notifications/NotificationPageStyles";
import { notificationTitle } from "../../../Styles/Notifications/NotificationStyles";
import { Appointment } from "../../Appointments/Appointment";
import { Doctor } from "../../Doctors/Doctor";
import { Page } from "../../General/Page";

export const NotificationPage = () => {
  const { notificationId } = useParams();
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();
  const userToken = useSelector((state: AppState) => state.identity.token);
  const notification = useSelector(
    (state: AppState) => state.notifiactions.notification,
  );
  const notificationLoading = useSelector(
    (state: AppState) => state.notifiactions.loading,
  );
  const appointment = useSelector(
    (state: AppState) => state.appointments.appointment,
  );
  const appointmentLoading = useSelector(
    (state: AppState) => state.appointments.loading,
  );
  const doctor = useSelector((state: AppState) => state.doctors.doctor);
  const doctorLoading = useSelector((state: AppState) => state.doctors.loading);

  useEffect(() => {
    const doGetNotification = async (notificationId?: string) => {
      dispatch(gettingNotificationAction());
      let result = await getNotification(notificationId, userToken);
      if (result.status === 401) dispatch(signOutUserAction(location.pathname));
      else if (result.status === 200)
        dispatch(gotNotificationAction(result.data));
    };
    doGetNotification(notificationId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    const doGetAppointment = async (appointmentId?: string) => {
      dispatch(gettingAppointmentAction());
      let result = await getAppointment(appointmentId, userToken);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      else if (result.responseStatus === 200)
        dispatch(gotAppointmentAction(result.data));
    };
    const doGetDoctor = async (doctorId?: string) => {
      dispatch(gettingDoctorAction());
      let result = await getDoctor(userToken, doctorId);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      else if (result.responseStatus === 200)
        dispatch(gotDoctorAction(result.data));
    };
    if (
      notification !== undefined &&
      notification !== null &&
      notification.appointmentId !== null
    )
      doGetAppointment(notification.appointmentId);
    else if (
      notification !== undefined &&
      notification !== null &&
      notification.message.includes("????????????: ")
    ) {
      let doctorId = notification.message
        .split(". ")[1]
        .replace("????????????: ", "");
      doGetDoctor(doctorId);
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [notification]);

  const onAppointmentConfirm = async () => {
    confirmAlert({
      customUI: ({ onClose }) => {
        return (
          <div css={confirmAlertContainer}>
            <h1 css={confirmAlertTitle}>???? ???????????????</h1>
            <p css={confirmAlertMessage}>
              ?????? ?????????????????? ?????????????????? ????{" "}
              {appointment?.startDate.toString().substring(0, 10)}{" "}
              {appointment?.startTime}
            </p>
            <FormButtonContainer className="row justify-content-around">
              <SecondaryButton
                className="btn btn-primary col-4 row"
                onClick={onClose}
              >
                ??????
              </SecondaryButton>
              <PrimaryButton
                className="btn btn-primary col-7 row"
                onClick={async () => {
                  var result = await confirmNotification(
                    notificationId,
                    userToken,
                  );
                  if (result) navigate("/notifications");
                  onClose();
                }}
              >
                ????, ??????????????????????!
              </PrimaryButton>
            </FormButtonContainer>
          </div>
        );
      },
    });
  };

  const onAppointmentReject = async () => {
    confirmAlert({
      customUI: ({ onClose }) => {
        return (
          <div css={confirmAlertContainer}>
            <h1 css={confirmAlertTitle}>???? ???????????????</h1>
            <p css={confirmAlertMessage}>
              ?????? ?????????????????? ?????????????????? ????{" "}
              {appointment?.startDate.toString().substring(0, 10)}{" "}
              {appointment?.startTime}
            </p>
            <FormButtonContainer className="row justify-content-around">
              <SecondaryButton
                className="btn btn-primary col-4 row"
                onClick={onClose}
              >
                ??????
              </SecondaryButton>
              <DangerButton
                className="btn btn-primary col-7 row"
                onClick={async () => {
                  var result = await rejectNotification(
                    notificationId,
                    userToken,
                  );
                  if (result) navigate("/notifications");
                  onClose();
                }}
              >
                ????, ??????????????????!
              </DangerButton>
            </FormButtonContainer>
          </div>
        );
      },
    });
  };

  const onNotificationDelete = async () => {
    var result = await deleteNotification(notificationId, userToken);
    if (result) navigate("/notifications");
  };

  return (
    <Page>
      {notificationLoading ? (
        <div>????????????????...</div>
      ) : (
        <div css={notificationContainer}>
          <div css={notificationDesctiption}>
            {notification?.message.split(". ")[0]}
          </div>
          {notification?.appointmentId !== null ? (
            <div>
              {appointmentLoading ? (
                <div>????????????????...</div>
              ) : (
                <div>
                  <div css={notificationTitle}>??????????????????:</div>
                  <div css={notificationAppointment}>
                    <Appointment appointment={appointment} />
                  </div>
                  {notification?.message.split(". ")[0] ===
                  "?????? ???????????????? ?????????? ???????????? ???? ??????????????????" ? (
                    <FormButtonContainer className="d-flex justify-content-around">
                      <PrimaryButton onClick={onAppointmentConfirm}>
                        ?????????????? ??????????????????
                      </PrimaryButton>
                      <DangerButton onClick={onAppointmentReject}>
                        ?????????????????? ??????????????????
                      </DangerButton>
                    </FormButtonContainer>
                  ) : (
                    <FormButtonContainer className="d-flex justify-content-around">
                      <DangerButton onClick={onNotificationDelete}>
                        ?????????????? ??????????????????????
                      </DangerButton>
                    </FormButtonContainer>
                  )}
                </div>
              )}
            </div>
          ) : (
            <div>
              <div css={notificationTitle}>????????????????:</div>
              <div css={notificationDoctor}>
                {doctorLoading ? (
                  <div>????????????????...</div>
                ) : (
                  <Doctor doctor={doctor} />
                )}
              </div>
              <FormButtonContainer className="d-flex justify-content-around">
                <DangerButton onClick={onNotificationDelete}>
                  ?????????????? ??????????????????????
                </DangerButton>
              </FormButtonContainer>
            </div>
          )}
        </div>
      )}
    </Page>
  );
};
