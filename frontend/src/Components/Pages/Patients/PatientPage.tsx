/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { confirmAlert } from "react-confirm-alert";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { deleteUser, UserData } from "../../../Api/IdentityData";
import { getPatient } from "../../../Api/PatientsData";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
import {
  gettingPatientAction,
  gotPatientAction,
} from "../../../Store/ActionCreators/PatientActionCreators";
import { AppState } from "../../../Store/Reducers/RootReducer";
import {
  DangerButton,
  FormButtonContainer,
  PrimaryButton,
  SecondaryButton,
} from "../../../Styles/Common/Buttons";
import { doctorAdditionalInf } from "../../../Styles/Doctors/DoctorStyles";
import {
  patientContainer,
  patientTitle,
} from "../../../Styles/Patient/PatientPageStyles";
import { Page } from "../../General/Page";

import "react-confirm-alert/src/react-confirm-alert.css";
import {
  confirmAlertContainer,
  confirmAlertMessage,
  confirmAlertTitle,
} from "../../../Styles/Common/OtherComponents";

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
    doGetPatient(loginedUser?.id);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [loginedUser]);

  const editClickHandler = () => {
    navigate("/patients/" + patient?.id + "/edit");
  };

  const deleteClickHandler = async () => {
    confirmAlert({
      customUI: ({ onClose }) => {
        return (
          <div css={confirmAlertContainer}>
            <h1 css={confirmAlertTitle}>???? ???????????????</h1>
            <p css={confirmAlertMessage}>?????? ???????????????? ????????????????????</p>
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
                  let result = await deleteUser(
                    user.userName === undefined ? "" : user.userName,
                    userToken === undefined ? "" : userToken,
                  );
                  if (result) dispatch(signOutUserAction("/"));
                  onClose();
                }}
              >
                ????, ??????????????!
              </DangerButton>
            </FormButtonContainer>
          </div>
        );
      },
    });
  };

  return (
    <Page>
      {patientLoading ? (
        <div>????????????????...</div>
      ) : (
        <div>
          <div css={patientContainer}>
            <div css={patientTitle}>
              {patient === null
                ? ""
                : `${patient?.firstName} ${patient?.lastName}`}
            </div>
            <div>
              ???????? ????????????????: {patient?.dateOfBurth.toString().substring(0, 10)}
            </div>
            <div css={doctorAdditionalInf}>
              <div>???????????????????? ????????????????????:</div>
              <div>??????????: {patient?.email}</div>
              <div>??????????????: {patient?.phoneNumber}</div>
            </div>
            <div css={doctorAdditionalInf}>
              <div>????????????: {patient?.country}</div>
              <div>??????????: {patient?.city}</div>
            </div>
          </div>
          {loginedUser?.id !== undefined &&
            patient?.userId === loginedUser?.id && (
              <div className="d-flex justify-content-center">
                <FormButtonContainer className="col-9 d-flex justify-content-around">
                  <PrimaryButton onClick={editClickHandler}>
                    ?????????????????????????? ??????????????
                  </PrimaryButton>
                  <DangerButton onClick={deleteClickHandler}>
                    ?????????????? ????????????????????????
                  </DangerButton>
                </FormButtonContainer>
              </div>
            )}
        </div>
      )}
    </Page>
  );
};
