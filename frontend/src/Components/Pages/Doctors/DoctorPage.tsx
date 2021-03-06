/** @jsxImportSource @emotion/react */
import { useEffect } from "react";
import { confirmAlert } from "react-confirm-alert";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { getDoctor } from "../../../Api/DoctorsData";
import { deleteUser, UserData } from "../../../Api/IdentityData";
import {
  gettingDoctorAction,
  gotDoctorAction,
} from "../../../Store/ActionCreators/DoctorActionCreators";
import { signOutUserAction } from "../../../Store/ActionCreators/IdentityActionCreators";
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
  doctorContainer,
  doctorTitle,
} from "../../../Styles/Doctors/DoctorPageStyles";
import {
  doctorAdditionalInf,
  doctorExp,
  doctorSpecialzation,
} from "../../../Styles/Doctors/DoctorStyles";
import { Page } from "../../General/Page";

type Props = {
  user: UserData;
};

export const DoctorPage = ({ user }: Props) => {
  const dispatch = useDispatch();
  const location = useLocation();
  const navigate = useNavigate();
  const doctor = useSelector((state: AppState) => state.doctors.doctor);
  const doctorLoading = useSelector((state: AppState) => state.doctors.loading);
  const loginedUser = useSelector((state: AppState) => state.identity.user);
  const userToken = useSelector((state: AppState) => state.identity.token);

  useEffect(() => {
    const doGetDoctor = async (userId?: string) => {
      dispatch(gettingDoctorAction());
      let result = await getDoctor(userToken, userId);
      if (result.responseStatus === 401)
        dispatch(signOutUserAction(location.pathname));
      dispatch(gotDoctorAction(result.data));
    };
    doGetDoctor(user.id);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const editClickHandler = () => {
    navigate("/doctors/" + doctor?.id + "/edit");
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
      {doctorLoading ? (
        <div>????????????????...</div>
      ) : (
        <div>
          <div css={doctorContainer}>
            <div css={doctorTitle}>
              {doctor === null
                ? ""
                : `${doctor?.firstName} ${doctor?.lastName}`}
            </div>
            <div>
              ???????? ????????????????: {doctor?.dateOfBurth.toString().substring(0, 10)}
            </div>
            <div css={doctorSpecialzation}>
              ??????????????????????????: {doctor?.specialization}
            </div>
            <div css={doctorExp}>????????: {doctor?.yearsOfExperience} ??????</div>
            <div css={doctorAdditionalInf}>
              <div>???????????????????? ????????????????????:</div>
              <div>??????????: {doctor?.email}</div>
              <div>??????????????: {doctor?.phoneNumber}</div>
            </div>
            <div css={doctorAdditionalInf}>
              <div>?????????? ????????????????: {doctor?.licenseNo}</div>
              <div>????????????: {doctor?.country}</div>
              <div>??????????: {doctor?.city}</div>
              <div>???????????????? ???? ????????????: {doctor?.workAddress}</div>
            </div>
          </div>
          {loginedUser?.id !== undefined && doctor?.userId === loginedUser?.id && (
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
          {loginedUser?.id !== undefined && loginedUser.type === "Patient" && (
            <div className="d-flex justify-content-center">
              <FormButtonContainer className="col-9 d-flex justify-content-around">
                <PrimaryButton
                  onClick={() => {
                    navigate("/newAppointment/" + doctor?.id);
                  }}
                >
                  ???????????????????? ???? ??????????
                </PrimaryButton>
              </FormButtonContainer>
            </div>
          )}
        </div>
      )}
    </Page>
  );
};
