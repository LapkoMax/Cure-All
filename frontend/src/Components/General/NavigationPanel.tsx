/** @jsxImportSource @emotion/react */

import { Fragment, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { getUserUnreadNotificationsAmount } from "../../Api/NotificationsData";
import { getPatient, PatientData } from "../../Api/PatientsData";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
import { AppState } from "../../Store/Reducers/RootReducer";
import {
  FormButtonContainer,
  PrimaryButton,
} from "../../Styles/Common/Buttons";
import {
  navigationContainer,
  notificationSpan,
} from "../../Styles/General/NavigationStyles";
import { PageTitle } from "./PageTitle";

export const NavigationPanel = () => {
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();
  const user = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [loginedPatient, setLoginedPatient] = useState<PatientData | null>(
    null,
  );
  const [unreadedAmount, setUnreadedAmount] = useState<number>(0);

  useEffect(() => {
    const doGetPatient = async () => {
      var result = await getPatient(userToken, user?.id);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      setLoginedPatient(result.data);
    };
    if (user?.type === "Patient") doGetPatient();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user]);

  useEffect(() => {
    const doGetUnreadedAmount = async (userId?: string) => {
      var result = await getUserUnreadNotificationsAmount(userId, userToken);
      setUnreadedAmount(result);
    };
    doGetUnreadedAmount(user?.id);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [location.pathname]);

  return (
    <div>
      <FormButtonContainer
        css={navigationContainer}
        className="row d-flex justify-content-center"
      >
        <PageTitle>Навигация:</PageTitle>
        <PrimaryButton
          className="btn btn-primary mb-2"
          onClick={() => {
            navigate("notifications");
          }}
        >
          Уведомления{" "}
          {unreadedAmount > 0 && (
            <span css={notificationSpan} className="badge badge-light">
              {unreadedAmount}
            </span>
          )}
        </PrimaryButton>
        {user?.type === "Doctor" && (
          <PrimaryButton
            className="col-12 row btn btn-primary mb-2"
            onClick={() => {
              navigate("appointments/" + user.id);
            }}
          >
            Ваши посещения
          </PrimaryButton>
        )}
        {user?.type === "Patient" && (
          <Fragment>
            <PrimaryButton
              className="col-12 row btn btn-primary mb-2"
              onClick={() => {
                navigate("doctors");
              }}
            >
              Список докторов
            </PrimaryButton>
            <PrimaryButton
              className="col-12 row btn btn-primary mb-2"
              onClick={() => {
                navigate("patientCard/" + loginedPatient?.patientCardId);
              }}
            >
              Ваша карта
            </PrimaryButton>
          </Fragment>
        )}
      </FormButtonContainer>
    </div>
  );
};
