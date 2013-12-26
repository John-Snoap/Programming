function connectfour(varargin)
% CONNECTFOUR
% 
% An implementation of the classic Connect Four by Milton Bradley.  Your
% goad is to arrange 4 chips in a row either vertically, horizontally, or
% diagonally before your opponent does.
%
% To play, simply click on the column in which you want to drop a peice.
% The computer will then automatically make its move.
%
% THE COMPUTER IS BEATABLE!  This is NOT the perfect Connect Four algorithm 
% that you may see on some websites.  The algorithm is good enough to win
% its fair number of matches.
%
% You can also click the "Demo" button to watch the computer play itself.
%
% ****************************
% Written by: Mickey Stahl
% December 5, 2001

if nargin==0,
    handles.fig=openfig('connectfour.fig');
    micintro
    set(handles.fig,'WindowButtonDownFcn',[mfilename '(''boardclick'',guidata(gcbo))'])
    set(get(handles.fig,'children'),'visible','on')
    handles=guihandles(handles.fig);
    handles.turn=1;
    handles.demomode=0;
    handles.board=zeros(6,7);
    handles.snapshot=handles.board;
    drawboard(handles)
    guidata(handles.ax,handles)
else
    feval(varargin{:});
end

function demofcn(handles)
set(handles.Demo,'enable','off')
set(handles.message,'string','Demo Game.')
handles.demomode=1;
handles.board=zeros(6,7);
handles.snapshot=handles.board;
handles.turn=ceil(2*rand);
guidata(handles.fig,handles)
set(handles.fig,'WindowButtonDownFcn','')
set(handles.message,'string','Demo Game.')
drawboard(handles)
handles.board(end,ceil(7*rand))=handles.turn;
handles=addpeice(handles);
handles.turn=2/handles.turn;
test=handles;
try
    while ~checkifend(handles) & test.demomode,
        handles=halzmove(handles);
        handles=addpeice(handles);
        handles.turn=2/handles.turn;
        test=guidata(handles.fig);
    end
catch
end

function w=checkifend(handles)
[r,c]=size(handles.board);
[w,ind]=check4win(handles.board);
if w,
    if w<3,
        set(handles.message,'string',sprintf('Player %d wins!',w))
        plot(ind(2,:)-0.5,r-ind(1,:)+0.5,'b','linewidth',3)
    else
        set(handles.message,'string',sprintf('It''s a draw!',w))
    end
    set(handles.fig,'windowbuttondownfcn','')
end

function resetfcn(handles)
set(handles.Demo,'enable','on')
handles.board=zeros(6,7);
handles.snapshot=handles.board;
handles.turn=1;
handles.demomode=0;
set(handles.fig,'WindowButtonDownFcn',[mfilename '(''boardclick'',guidata(gcbo))'])
set(handles.message,'string','Player 1 choose a column.')
drawboard(handles)
guidata(handles.fig,handles)

function boardclick(handles)
set(handles.fig,'WindowButtonDownFcn','')
point=get(handles.ax,'currentpoint');
point=point(1,1:2);
board=handles.board;
[r,c]=size(board);
try
    if point(1)>0 & point(1)<c & point(2)>0 & point(2)<r,
        point=floor(point)+1;
        handles.turn=1;
        [handles.board,result]=makemove(handles.board,1,point(1));
        if result,
            handles=addpeice(handles);
            handles.turn=2;
            if ~checkifend(handles),
                handles=halzmove(handles);
                handles=addpeice(handles);
                checkifend(handles);
            end
        end
    end
    guidata(handles.fig,handles)
    set(handles.fig,'WindowButtonDownFcn',[mfilename '(''boardclick'',guidata(gcbo))'])
catch
end

function handles=halzmove(handles)
board=handles.board;
[r,c]=size(board);

oponent=2/handles.turn;
for j=1:c,
    [possibleBoard,result]=makemove(handles.board,handles.turn,j);
    w=check4win(possibleBoard);
    if w==handles.turn,
        handles.board=possibleBoard;
        return
    end
end
for j=1:c,
    [possibleBoard,result]=makemove(handles.board,oponent,j);
    w=check4win(possibleBoard);
    if w==oponent,
        handles.board=makemove(handles.board,handles.turn,j);;
        return
    end
end
if length(find(handles.board==handles.turn))<3,
    offense=0;
else
    if handles.demomode,
        offense=round(1-rand^3);
    else
        offense=round(rand);
    end
end
for j=1:c,
    [nextBoard,result(j)]=makemove(handles.board,handles.turn,j);        
    score(j)=scoreboard(nextBoard,handles.turn,offense);
end
score(~result)=-inf;
moves=find(score==max(score));
nextMove=ceil(length(moves)*rand);
handles.board=makemove(handles.board,handles.turn,moves(nextMove));


function score=scoreboard(board,turn,offense)
oponent=2/turn;
if offense,
    score=maxinarow(board,turn);
else
    score=1/maxinarow(board,oponent);
end
for j=1:size(board,2),
    nextBoard=makemove(board,oponent,j);
    w=check4win(nextBoard);
    if w==oponent,score=-1;end
end

function drawboard(handles)
board=handles.board;
[r,c]=size(board);
axes(handles.ax),cla,hold on
radius=0.35;
t=-pi/2:0.01:pi/2;
x1=[0 0 radius.*cos(t) 0 0 0.5 0.5 0];
y1=[-0.5 -radius radius.*sin(t) radius 0.5 0.5 -0.5 -0.5];
x2=[0 0 -radius.*cos(t) 0 0 -0.5 -0.5 0];
for j=1:c,
    for k=1:r,
        patch(x1+j-0.5,y1+r-k+0.5,'y','edgecolor','none'),axis equal
        patch(x2+j-0.5,y1+r-k+0.5,'y','edgecolor','none')
    end
end
axis([0 c 0 r],'equal','off')

function handles=addpeice(handles)

board=handles.board;
[r,c]=size(board);
axes(handles.ax),hold on
radius=0.35;
t=0:0.01:2*pi;
x=radius.*cos(t);
y=radius.*sin(t);
[m,n]=find(board~=handles.snapshot);
colors=[1 0 0;0 0 0];
start=r+1;
finish=r-m+0.5;
step=0.5;
h=patch(x+n-0.5,start,colors(board(m,n),:));
kids=get(handles.ax,'children');
set(handles.ax,'children',[kids(2:end);kids(1)])
fall=start;
while fall>finish & ishandle(h),fall=fall-step;set(h,'ydata',y+fall),drawnow,end
s=get(handles.fig,'userdata');
sound(s.y2,s.fs)
handles.snapshot=board;

function micintro

authorzname='Mickey Stahl';
MatlabCentral='Matlab Central';
url='http://www.mathworks.com/matlabcentral/';


ax=axes(...
    'units','norm',...
    'position',[0 0 1 1],...
    'xtick',[],'ytick',[],...
    'color',[0.8 0.8 1]...
    );

h1=letterzoom(authorzname,0.7,0.3,0.7,0.1,'k');
h2=letterzoom(MatlabCentral,0.55,0.15,0.85,0.12,'k');
h3=letterzoom(url,0.4,0.1,0.9,0.04,'b');
pause(1.5)
s=get(gcf,'userdata');
sound(s.y1,s.fs)
scatterletters([h1 h2 h3])
delete(ax)

function scatterletters(h)
n=length(h);
finalx=rand(1,n)-0.5;
finaly=rand(1,n)-0.5;
finalx=finalx+0.5.*sign(finalx)+0.5;
finaly=finaly+0.5.*sign(finaly)+0.5;
stepz=20;
for j=1:n,
    pos=get(h(j),'pos');
    x=pos(1); y=pos(2);
    d(j,:)=[(finalx(j)-x)/stepz (finaly(j)-y)/stepz];
end
moveletters(h,d,stepz)

function h=letterzoom(string,height,xleft,xright,fontsize,color);
n=length(string);
finalx=linspace(xleft,xright,n);
finaly=finalx.*0+height;
stepz=20;
for j=1:n,
    x=rand;
    y=rand;
    if x<0.5,x=x-0.5;else x=x+0.5;end
    if y<0.5,y=y-0.5;else y=y+0.5;end
    h(j)=text(x,y,string(j),...
        'fontunits','normalized',...
        'fontsize',fontsize,...
        'color',color,...
        'fontname','Courier New',...
        'fontweight','bold',...
        'HorizontalAlignment','center',...
        'erasemode','xor');
    d(j,:)=[(finalx(j)-x)/stepz (finaly(j)-y)/stepz];
end
axis([0 1 0 1])
moveletters(h,d,stepz)

function moveletters(h,d,stepz)
n=length(h);
for j=1:stepz,
    for k=1:n,
        currentPos=get(h(k),'position');
        currentPos(1:2)=currentPos(1:2)+d(k,:);
        set(h(k),'position',currentPos)
    end
    pause(1/stepz)
end

function [y,fs]=whistle(cf);
fs=10000;
t=0:1/fs:1.4;
mod=cf+100.*sawtooth(100*pi.*t)-(cf/2).*t./max(t);
y=sin(2*pi.*cumtrapz(mod)./fs);

function n = maxinarow(board,player)
w=check4win(board);
if w==player,n=4;
else
    n=0;
    [r,c]=find(board==player);
    horz_step=[-1 0 1 1 1 0 -1 -1];
    vert_step=[1 1 1 0 -1 -1 -1 0];
    n=0;
    for j=1:length(r),
        for k=1:8,
            if r(j)-3*vert_step(k)>0 & r(j)-3*vert_step(k)<=size(board,1) & c(j)+3*horz_step(k)>0 & c(j)+3*horz_step(k)<=size(board,2),
                if vert_step(k)~=0,
                    checkrows=r(j):-vert_step(k):r(j)-3*vert_step(k);
                else
                    checkrows=r(j).*ones(1,4);
                end
                if horz_step(k)~=0,
                    checkcols=c(j):horz_step(k):c(j)+3*horz_step(k);
                else
                    checkcols=c(j).*ones(1,4);
                end
                for m=1:4,
                    group(m)=board(checkrows(m),checkcols(m));
                end
                if ~isempty(find(group==2/player))
                    temp=0;
                else
                    peices=find(group==player);
                    switch length(peices)
                    case 1
                        temp=1;
                    case 2
                        if diff(peices)==1,
                            temp=2;
                        else
                            temp=1;
                        end
                    case 3
                        if group(1)==player & group(4)==player,
                            temp=2;
                        else
                            temp=3;
                        end
                    end
                end
                n=n+10^temp;
            end
        end
    end
end

function [w,ind] = check4win(b)

[w,ind]=checkplayer(b,1);
if w==0,[w,ind]=checkplayer(b,2); end
if isempty(find(b==0)) & w==0,w=3;end

function [w,ind]=checkplayer(board,player)

[r,c]=find(board==player);
horz_step=[-1 0 1 1 1 0 -1 -1];
vert_step=[1 1 1 0 -1 -1 -1 0];
w=0;
ind=[];
for j=1:length(r),
    for k=1:8,
        if r(j)-3*vert_step(k)>0 & r(j)-3*vert_step(k)<=size(board,1) & c(j)+3*horz_step(k)>0 & c(j)+3*horz_step(k)<=size(board,2),
            if vert_step(k)~=0,
                checkrows=r(j):-vert_step(k):r(j)-3*vert_step(k);
            else
                checkrows=r(j).*ones(1,4);
            end
            if horz_step(k)~=0,
                checkcols=c(j):horz_step(k):c(j)+3*horz_step(k);
            else
                checkcols=c(j).*ones(1,4);
            end
            for m=1:4,
                group(m)=board(checkrows(m),checkcols(m));
            end
            if sum(abs(group-player))==0,
                ind=[checkrows;checkcols];
                w=player;
                return
            end
        end
    end
end

function [b,result] = makemove(b,player,col)

if b(1,col)~=0,
    result=0;
    return
end

for j=1:size(b,1),
    if b(j,col)~=0, j=j-1; break, end
end
b(j,col)=player;
result=1;