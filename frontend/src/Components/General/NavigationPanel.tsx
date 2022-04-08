/** @jsxImportSource @emotion/react */

import { useSelector } from "react-redux";
import { AppState } from "../../Store/Reducers/RootReducer";
import {
  FormButtonContainer,
  PrimaryButton,
} from "../../Styles/Common/Buttons";
import { navigationContainer } from "../../Styles/General/NavigationStyles";
import { PageTitle } from "./PageTitle";

export const NavigationPanel = () => {
  const user = useSelector((state: AppState) => state.identity.user);

  return (
    <div>
      <FormButtonContainer
        css={navigationContainer}
        className="row d-flex justify-content-center"
      >
        <PageTitle>Навигация:</PageTitle>
        {user?.type === "Doctor" && (
          <PrimaryButton>Ваши посещения</PrimaryButton>
        )}
      </FormButtonContainer>
    </div>
  );
};
