import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import {
  CreateIllnessForm,
  createNewIllness,
  getIllneses,
  IllnessData,
} from "../../Api/IllnesesData";
import { signOutUserAction } from "../../Store/ActionCreators/IdentityActionCreators";
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
  returnUrl: string;
};

export const CreateIllness = ({ returnUrl }: Props) => {
  const navigate = useNavigate();
  const location = useLocation();
  const {
    register,
    formState: { errors, isSubmitting },
    handleSubmit,
  } = useForm<CreateIllnessForm>({ mode: "onBlur" });
  const dispatch = useDispatch();
  const userToken = useSelector((state: AppState) => state.identity.token);
  const [createErrors, setCreateErrors] = useState<string[] | undefined>([]);
  const [illneses, setIllneses] = useState<IllnessData[]>([]);

  useEffect(() => {
    const doGetIllneses = async () => {
      let results = await getIllneses();
      setIllneses(results);
    };
    doGetIllneses();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const submitForm = async (data: CreateIllnessForm) => {
    setCreateErrors([]);
    const result = await createNewIllness(data, userToken);
    if (result.length === 0) {
      navigate(returnUrl);
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
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="name">Название</FieldLabel>
          <FieldInput
            id="name"
            {...register("name", {
              required: "Название обязательно!",
              minLength: {
                value: 3,
                message: "Название должно состоять минимум из 3 символов!",
              },
              maxLength: {
                value: 50,
                message: "Название должно состоять максимум из 50 символов!",
              },
              validate: {
                uniqueName: (value) => {
                  let names = illneses.map((illness) => illness.name + ", ");
                  return (
                    !names.includes(value) ||
                    "Заболевание с таким именем уже существует!"
                  );
                },
              },
            })}
            type="text"
          />
          <FieldError>{errors.name?.message}</FieldError>
        </FieldContainer>
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
                value: 300,
                message: "Описание должно состоять максимум из 300 символов!",
              },
            })}
          />
          <FieldError>{errors.description?.message}</FieldError>
        </FieldContainer>
        <FieldContainer className="row col-12 d-flex justify-content-center">
          <FieldLabel htmlFor="symptoms">Симптомы</FieldLabel>
          <FieldTextArea
            id="symptoms"
            {...register("symptoms", {
              required: "Симптомы обязательно!",
              minLength: {
                value: 3,
                message:
                  "Описание симптомов должно состоять минимум из 2 символов!",
              },
              maxLength: {
                value: 150,
                message:
                  "Описание симптомов должно состоять максимум из 150 символов!",
              },
            })}
          />
          <FieldError>{errors.symptoms?.message}</FieldError>
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
            Сохранить данные
          </PrimaryButton>
          <SecondaryButton
            onClick={() => {
              navigate(returnUrl);
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
