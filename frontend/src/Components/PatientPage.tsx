/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { deleteUser, UserData } from "../Api/IdentityData";
import { getPatient } from "../Api/PatientsData";
import { signOutUserAction } from "../Store/ActionCreators/IdentityActionCreators";
import {
  gettingPatientAction,
  gotPatientAction,
} from "../Store/ActionCreators/PatientActionCreators";
import { AppState } from "../Store/Reducers/RootReducer";
import {
  DangerButton,
  FormButtonContainer,
  PrimaryButton,
} from "../Styles/Common/Buttons";
import { doctorAdditionalInf } from "../Styles/Doctors/DoctorStyles";
import {
  patientContainer,
  patientTitle,
} from "../Styles/Patient/PatientPageStyles";
import { Page } from "./General/Page";

type Props = {
  user: UserData;
};

export const PatientPage = ({ user }: Props) => {
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();
  const patient = useSelector((state: AppState) => state.patients.patient);
  const patientLoading = useSelector(
    (state: AppState) => state.patients.loading,
  );
  const loginedUser = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetPatient = async (userId?: string) => {
      dispatch(gettingPatientAction());
      let result = await getPatient(userToken, userId);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotPatientAction(result.data));
    };
    doGetPatient(user.id);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const editClickHandler = () => {
    navigate("/patients/" + patient?.id + "/edit");
  };

  const deleteClickHandler = async () => {
    let result = await deleteUser(
      user?.userName === undefined ? "" : user?.userName,
      userToken === undefined ? "" : userToken,
    );
    if (result) dispatch(signOutUserAction("/"));
  };

  return (
    <Page>
      {patientLoading ? (
        <div>Загрузка...</div>
      ) : (
        <div>
          <div css={patientContainer}>
            <div css={patientTitle}>
              {patient === null
                ? ""
                : `${patient?.firstName} ${patient?.lastName}`}
            </div>
            <div>
              Дата рождения: {patient?.dateOfBurth.toString().substring(0, 10)}
            </div>
            <div css={doctorAdditionalInf}>
              <div>Контактная информация:</div>
              <div>Почта: {patient?.email}</div>
              <div>Телефон: {patient?.phoneNumber}</div>
            </div>
            <div css={doctorAdditionalInf}>
              <div>Страна: {patient?.country}</div>
              <div>Город: {patient?.city}</div>
            </div>
          </div>
          {loginedUser?.id !== undefined &&
            patient?.userId === loginedUser?.id && (
              <div className="d-flex justify-content-center">
                <FormButtonContainer className="col-9 d-flex justify-content-around">
                  <PrimaryButton onClick={editClickHandler}>
                    Редактировать профиль
                  </PrimaryButton>
                  <DangerButton onClick={deleteClickHandler}>
                    Удалить пользователя
                  </DangerButton>
                </FormButtonContainer>
              </div>
            )}
        </div>
      )}
    </Page>
  );
};
